using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using OpenTibia.Client.Sprites;
using System.Linq;

namespace SpiderClientConverter
{
    public partial class SpiderClientConverter : Form
    {
        public class Catalog
        {
            public string Type { get; set; }
            public string File { get; set; }
            public int SpriteType { get; set; }
            public int FirstSpriteid { get; set; }
            public int LastSpriteid { get; set; }
        }

        BackgroundWorker worker = new BackgroundWorker();
        public List<Catalog> catalog;
        private static string _assetsPath = "";
        private static string _dumpToPath = "";
        public OpenTibia.Core.Version version = new OpenTibia.Core.Version(1000, "Client 10.00", 0x4A10, 0x59E48E02, 0);
        public OpenTibia.Client.ClientFeatures Features;
        public SpriteStorage sprites;
        public int progress;

        public uint Signature { get; set; }
        public ushort ObjectCount { get; set; }
        public ushort OutfitCount { get; set; }
        public ushort EffectCount { get; set; }
        public ushort MissileCount { get; set; }
        public List<int> SpritesOffset { get; set; }

        public SpiderClientConverter()
        {
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_Completed;
        }

        private void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 0;
            Percent.Text = "100%";
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((int)e.Argument == 1)
                LoadSprites();
            if ((int)e.Argument == 2)
                LoadAppearances();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            Percent.Text = progressBar.Value.ToString() + "%";
        }

        private void AssetsF_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _assets = new FolderBrowserDialog();
            if (_assets.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _assetsPath = _assets.SelectedPath;
                if (_assetsPath.EndsWith("\\") == false)
                    _assetsPath = _assetsPath + "\\";
                AssetsPath.Text = _assetsPath;
            }
        }

        private void OutputF_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _dumpTo = new FolderBrowserDialog();
            if (_dumpTo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _dumpToPath = _dumpTo.SelectedPath;
                if (_dumpToPath.EndsWith("\\") == false)
                    _dumpToPath = _dumpToPath + "\\";
                OutputPath.Text = _dumpToPath;
            }
        }

        private void ExportSheets_Click(object sender, EventArgs e)
        {
            if (_assetsPath != "" && _dumpToPath != "" && File.Exists(_assetsPath + "catalog-content.json") == true)
            {
                if (worker.IsBusy == false)
                    worker.RunWorkerAsync(argument: 1);
            }
            else
                MessageBox.Show("Couldn't find assets path, output path or catalog-content.");
        }

        private void ExportDat_Click(object sender, EventArgs e)
        {
            if (_assetsPath != "" && _dumpToPath != "" && File.Exists(_assetsPath + "catalog-content.json") == true)
            {
                if (worker.IsBusy == false)
                    worker.RunWorkerAsync(argument: 2);
            }
            else
                MessageBox.Show("Couldn't find assets path, output path or catalog-content.");

        }

        public void LoadCatalogJson()
        {
            using (StreamReader r = new StreamReader(_assetsPath + "catalog-content.json"))
            {
                string json = r.ReadToEnd();
                catalog = JsonConvert.DeserializeObject<List<Catalog>>(json);
            }
        }

        public void LoadSprites()
        {
            LoadCatalogJson();
            catalog.Sort((x, y) => x.FirstSpriteid.CompareTo(y.FirstSpriteid));
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount * 5
            };
            if (ToSpr.Checked || SliceBox.Checked)
                options = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 1
                };

            if (ToSpr.Checked)
            {
                Features = OpenTibia.Client.ClientFeatures.Extended;
                sprites = SpriteStorage.Create(version, Features);
            }
            progress = 0;
            Directory.CreateDirectory(_dumpToPath + @"//slices//");
            Parallel.ForEach(catalog, options, sheet =>
            {
                progress++;
                if (sheet.Type == "sprite")
                {
                    Console.WriteLine("{0}", sheet.File);
                    DumpSpriteSheet(sheet);
                }
                worker.ReportProgress((int)(progress * 100 / catalog.Count));
            });
            if (ToSpr.Checked)
                sprites.Save(String.Format("{0}//Clients//Tibia.spr", _dumpToPath), version);
                
        }

        public void LoadAppearances()
        {
            progress = 0;
            LoadCatalogJson();
            string datPath = String.Format("{0}{1}", _assetsPath, catalog[0].File);
            string datExportPath = String.Format("{0}//Clients//", _dumpToPath);

            if (File.Exists(datPath) == false)
                return;
            catalog.Sort((x, y) => x.FirstSpriteid.CompareTo(y.FirstSpriteid));
            using (Stream stream = File.OpenRead(datPath))
            {
                Appearances appearances = Appearances.Parser.ParseFrom(stream);

                Signature = 0x4A10;
                ObjectCount = (ushort)appearances.Object[appearances.Object.Count - 1].Id;
                OutfitCount = (ushort)appearances.Outfit[appearances.Outfit.Count - 1].Id;
                EffectCount = (ushort)appearances.Effect[appearances.Effect.Count - 1].Id;
                MissileCount = (ushort)appearances.Missile[appearances.Missile.Count - 1].Id;

                Console.WriteLine("ObjectCount: {0}", ObjectCount);
                Console.WriteLine("OutfitCount: {0}", OutfitCount);
                Console.WriteLine("EffectCount: {0}", EffectCount);
                Console.WriteLine("MissileCount: {0}", MissileCount);
                SetSpriteOffset();
                Directory.CreateDirectory(datExportPath);
                var datFile = new FileStream(datExportPath + "Tibia.dat", FileMode.Create, FileAccess.Write);
                using (var w = new BinaryWriter(datFile))
                {
                    w.Write(Signature);
                    w.Write(ObjectCount);
                    w.Write(OutfitCount);
                    w.Write(EffectCount);
                    w.Write(MissileCount);
                    int CurrentId = 0;
                    for (int i = 0; i <= appearances.Object[appearances.Object.Count-1].Id-100; i++)
                    {
                        if (i + 100 == appearances.Object[CurrentId].Id)
                        {
                            WriteAppearance1000(w, appearances.Object[CurrentId], 1);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00};
                            w.Write(buffer);
                        }
                    }
					CurrentId = 0;
                    for (int i = 1; i <= appearances.Outfit[appearances.Outfit.Count-1].Id; i++)
                    {
                        if (appearances.Outfit[CurrentId] != null && i == appearances.Outfit[CurrentId].Id)
                        {
                            WriteAppearance1000(w, appearances.Outfit[CurrentId], 2);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00};
                            w.Write(buffer);
                        }
                    }	
					CurrentId = 0;
                    for (int i = 1; i <= appearances.Effect[appearances.Effect.Count-1].Id; i++)
                    {
                        if (appearances.Effect[CurrentId] != null && i == appearances.Effect[CurrentId].Id)
                        {
                            WriteAppearance1000(w, appearances.Effect[CurrentId], 3);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00};
                            w.Write(buffer);
                        }
                    }	
					CurrentId = 0;
                    for (int i = 1; i <= appearances.Missile[appearances.Missile.Count-1].Id; i++)
                    {
                        if (appearances.Missile[CurrentId] != null && i == appearances.Missile[CurrentId].Id)
                        {
                            WriteAppearance1000(w, appearances.Missile[CurrentId], 4);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00};
                            w.Write(buffer);
                        }
                    }
                    datFile.Close();
                }
            }
        }

        public void DumpSpriteSheet(Catalog sheet)
        {
            string filePath = String.Format("{0}{1}.lzma", _assetsPath, sheet.File);
            if (File.Exists(_assetsPath + sheet.File) == false)
                return;

            MemoryStream _spriteBuffer = new MemoryStream(0x100000);
            MemoryStream spriteBuffer = _spriteBuffer;

            SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(_assetsPath + sheet.File)))
            {
                Stream input = reader.BaseStream;

                // CIP's header
                input.Position = 30; // Skip past 6 initial constant bytes
                while ((reader.ReadByte() & 0x80) == 0x80) { } // LZMA size, 7-bit integer where MSB = flag for next byte used

                // LZMA file
                decoder.SetDecoderProperties(reader.ReadBytes(5));
                reader.ReadUInt64();

                // Disabled arithmetic underflow/overflow check in debug mode so this won't cause an exception
                spriteBuffer.Position = 0;
                decoder.Code(input, spriteBuffer, input.Length - input.Position, 0x100000, null);
            }

            spriteBuffer.Position = 0;
            Image image = Image.FromStream(spriteBuffer);
            image.Save(String.Format("{0}Sprites {1}-{2}-{3}.png", _dumpToPath, sheet.FirstSpriteid.ToString(), sheet.LastSpriteid.ToString(), sheet.SpriteType.ToString()), ImageFormat.Png);

            if (ToSpr.Checked || SliceBox.Checked)
            {
                Size tileSize = new Size(32, 32);
                Point Offset = new Point(0, 0);
                Size Space = new Size(0, 0);
                ImageList imglist = new ImageList();
                imglist = GenerateTileSetImageList(image, tileSize, Offset, Space, sheet.SpriteType);
                imglist.ColorDepth = ColorDepth.Depth32Bit;
                imglist.ImageSize = new Size(32, 32);
                int loopTo = sheet.LastSpriteid - sheet.FirstSpriteid;
                if (sheet.SpriteType == 3)
                    loopTo = (loopTo * 4) + 4;
                else if (sheet.SpriteType >= 1)
                    loopTo = (loopTo * 2) + 2;
                else
                    loopTo += 1;
                for (int i = 0; i < loopTo; i++)
                {
                    if (ToSpr.Checked)
                        sprites.AddSprite((Bitmap)imglist.Images[i]);

                    if (SliceBox.Checked)
                    {
                        if (sheet.SpriteType == 0)
                            imglist.Images[i].Save(_dumpToPath + @"//slices//" + (sheet.FirstSpriteid + i).ToString() + ".png");
                        else
                            imglist.Images[i].Save(_dumpToPath + @"//slices//" + GetSprName(sheet.FirstSpriteid, i, sheet.SpriteType) + ".png");
                    }
                    imglist.Images[i].Dispose();
                }
            }
        }

        public string GetSprName(int Id, int tileId, int spriteType)
        {
            string sprName = "";
            if (spriteType <= 2)
            {
                double sprID = Id + Math.Floor((double)tileId / 2);
                sprName = sprID.ToString();
                sprID = (tileId / 2.0) - Math.Truncate((double)tileId / 2.0);
                if (sprID == 0.0)
                    sprName += "_1";
                else if (sprID == 0.5)
                    sprName += "_2";
            }
            else if (spriteType == 3)
            {
                double sprID = Id + Math.Floor((double)tileId / 4);
                sprName = sprID.ToString();

                sprID = (tileId/4.0) - Math.Truncate((double)tileId / 4.0);
                if(sprID == 0.0)
                    sprName += "_1";
                else if (sprID == 0.25)
                    sprName += "_2";
                else if (sprID == 0.5)
                    sprName += "_3";
                else if (sprID == 0.75)
                    sprName += "_4";
            }
            return sprName;
        }

        public void SetSpriteOffset()
        {
            int counter = 2;
            SpritesOffset = Enumerable.Range(0, 500000).ToList();
            foreach (Catalog sheet in catalog)
            {
                progress++;
                if (sheet.Type == "sprite")
                {
                    for (int i = 0; i <= (sheet.LastSpriteid - sheet.FirstSpriteid); i++)
                    {
                        SpritesOffset.Insert(sheet.FirstSpriteid + i, counter);
                        if (sheet.SpriteType == 0)
                            counter += 1;
                        else if (sheet.SpriteType == 1 || sheet.SpriteType == 2)
                            counter += 2;
                        else if (sheet.SpriteType == 3)
                            counter += 4;
                        worker.ReportProgress((int)(progress * 100 / (ObjectCount + OutfitCount + EffectCount + MissileCount + catalog.Count)));
                    }                        
                }
                
            }
        }

        public ImageList GenerateTileSetImageList(Image tileSetImage, Size tileSize, Point offset, Size space, int spriteType)
        {
            try
            {
                ImageList Tiles = new ImageList();
                Tiles.ImageSize = tileSize;

                float width = default(float), height = default(float);
                width = tileSetImage.PhysicalDimension.Width;
                height = tileSetImage.PhysicalDimension.Height;

                if ((tileSize.Width > 0) & (tileSize.Height > 0))
                {
                    if ((width >= (float)tileSize.Width) & (height >= (float)tileSize.Height))
                    {
                        Image tile = default(Image);
                        int xCounter = 0, yCounter = 0;
                        if (spriteType == 0)
                        {
                            for (int x = 0; x <= 11; x++)
                            {
                                for (int y = 0; y <= 11; y++)
                                {
                                    tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                    Graphics g = Graphics.FromImage(tile);
                                    Rectangle sourceRect = new Rectangle(y * tileSize.Width, x * tileSize.Height, tileSize.Width, tileSize.Height);
                                    g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                    Tiles.Images.Add(Convert.ToString(Tiles.Images.Count), tile);
                                    g.Dispose();
                                }
                            }
                        }
                        else if (spriteType == 1)
                        {
                            for (int x = 0; x <= 5; x++)
                            {
                                for (int y = 0; y <= 11; y++)
                                {
                                    ImageList ArrangeTiles = new ImageList();
                                    ArrangeTiles.ImageSize = tileSize;

                                    for (int a = 0; a <= 1; a++)
                                    {
                                        tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                        Graphics g = Graphics.FromImage(tile);
                                        Rectangle sourceRect = new Rectangle(y * tileSize.Width, (x + yCounter + a) * tileSize.Height, tileSize.Width, tileSize.Height);
                                        g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                        ArrangeTiles.Images.Add(Convert.ToString(ArrangeTiles.Images.Count), tile);
                                        g.Dispose();
                                    }
                                    if (xCounter < 11)
                                        xCounter = xCounter + 1;
                                    else
                                    {
                                        xCounter = 0;
                                        yCounter = yCounter + 1;
                                    }
                                    for (int k = 1; k >= 0; k--)
                                        Tiles.Images.Add(Convert.ToString(Tiles.Images.Count), ArrangeTiles.Images[k]);

                                }

                            }
                        }
                        else if (spriteType == 2)
                        {
                            for (int x = 0; x <= 11; x++)
                            {
                                for (int y = 0; y <= 5; y++)
                                {
                                    ImageList ArrangeTiles = new ImageList();
                                    ArrangeTiles.ImageSize = tileSize;

                                    for (int a = 0; a <= 1; a++)
                                    {
                                        tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                        Graphics g = Graphics.FromImage(tile);
                                        Rectangle sourceRect = new Rectangle(xCounter * tileSize.Width, x * tileSize.Height, tileSize.Width, tileSize.Height);
                                        g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                        ArrangeTiles.Images.Add(Convert.ToString(ArrangeTiles.Images.Count), tile);
                                        g.Dispose();

                                        if (xCounter < 11)
                                            xCounter = xCounter + 1;
                                        else
                                            xCounter = 0;
                                    }
                                    yCounter = yCounter + 1;
                                    for (int k = 1; k >= 0; k--)
                                        Tiles.Images.Add(Convert.ToString(Tiles.Images.Count), ArrangeTiles.Images[k]);
                                }

                            }
                        }
                        else if (spriteType == 3)
                        {
                            for (int x = 0; x <= 5; x++)
                            {
                                for (int y = 0; y <= 5; y++)
                                {
                                    ImageList ArrangeTiles = new ImageList();
                                    ArrangeTiles.ImageSize = tileSize;

                                    for (int a = 0; a <= 1; a++)
                                    {
                                        for (int b = 0; b <= 1; b++)
                                        {
                                            tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                            Graphics g = Graphics.FromImage(tile);
                                            Rectangle sourceRect = new Rectangle((y + b + xCounter) * tileSize.Width, (x + a + yCounter) * tileSize.Height, tileSize.Width, tileSize.Height);
                                            g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                            ArrangeTiles.Images.Add(Convert.ToString(ArrangeTiles.Images.Count), tile);
                                            g.Dispose();
                                        }
                                    }
                                    if (xCounter < 5)
                                        xCounter = xCounter + 1;
                                    else
                                    {
                                        xCounter = 0;
                                        yCounter = yCounter + 1;
                                    }
                                    for (int k = 3; k >= 0; k--)
                                        Tiles.Images.Add(Convert.ToString(Tiles.Images.Count), ArrangeTiles.Images[k]);
                                }
                            }
                        }
                    }
                }

                return Tiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return null;
        }

        public void WriteAppearance1000(BinaryWriter w, Appearance item, int type)
        {
            progress++;
            if (item.Flags.Ground != null)
            {
                w.Write((byte)AppearanceFlag1000.Ground);
                w.Write((ushort)item.Flags.Ground.GroundSpeed);
            }

            if (item.Flags.Clip)
                w.Write((byte)AppearanceFlag1000.Clip);

            if (item.Flags.Top)
                w.Write((byte)AppearanceFlag1000.Top);

            if (item.Flags.Bottom)
                w.Write((byte)AppearanceFlag1000.Bottom);

            if (item.Flags.Container)
                w.Write((byte)AppearanceFlag1000.Container);

            if (item.Flags.Cumulative)
                w.Write((byte)AppearanceFlag1000.Stackable);

            if (item.Flags.Forceuse)
                w.Write((byte)AppearanceFlag1000.ForceUse);

            if (item.Flags.Usable)
                w.Write((byte)AppearanceFlag1000.Usable);

            if (item.Flags.Multiuse)
                w.Write((byte)AppearanceFlag1000.MltiUse);

            if (item.Flags.Write != null)
            {
                w.Write((byte)AppearanceFlag1000.Writeable);
                w.Write((ushort)item.Flags.Write.MaxTextLength);
            }

            if (item.Flags.WriteOnce != null)
            {
                w.Write((byte)AppearanceFlag1000.WriteableOnce);
                w.Write((ushort)item.Flags.WriteOnce.MaxTextLengthOnce);
            }

            if (item.Flags.Liquidpool)
                w.Write((byte)AppearanceFlag1000.LiquidPool);

            if (item.Flags.Liquidcontainer)
                w.Write((byte)AppearanceFlag1000.LiquidContainer);

            if (item.Flags.Unpass)
                w.Write((byte)AppearanceFlag1000.Impassable);

            if (item.Flags.Unmove)
                w.Write((byte)AppearanceFlag1000.Unmovable);

            if (item.Flags.Unsight)
                w.Write((byte)AppearanceFlag1000.BlocksSight);

            if (item.Flags.Avoid)
                w.Write((byte)AppearanceFlag1000.BlocksPathfinding);

            if (item.Flags.NoMovementAnimation)
                w.Write((byte)AppearanceFlag1000.NoMovementAnimation);

            if (item.Flags.Take)
                w.Write((byte)AppearanceFlag1000.Pickupable);

            if (item.Flags.Hang)
                w.Write((byte)AppearanceFlag1000.Hangable);

            if (item.Flags.Hook != null)
            {
                if (item.Flags.Hook.Direction == HOOK_TYPE.South)
                    w.Write((byte)AppearanceFlag1000.HooksSouth);

                if (item.Flags.Hook.Direction == HOOK_TYPE.East)
                    w.Write((byte)AppearanceFlag1000.HooksEast);

            }

            if (item.Flags.Rotate)
                w.Write((byte)AppearanceFlag1000.Rotateable);

            if (item.Flags.Light != null)
            {
                w.Write((byte)AppearanceFlag1000.LightSource);
                w.Write((ushort)item.Flags.Light.Brightness);
                w.Write((ushort)item.Flags.Light.Color);
            }

            if (item.Flags.DontHide)
                w.Write((byte)AppearanceFlag1000.AlwaysSeen);

            if (item.Flags.Translucent)
                w.Write((byte)AppearanceFlag1000.Translucent);

            if (item.Flags.Shift != null)
            {
                w.Write((byte)AppearanceFlag1000.Displaced);
                w.Write((ushort)item.Flags.Shift.X);
                w.Write((ushort)item.Flags.Shift.Y);
            }

            if (item.Flags.Height != null)
            {
                w.Write((byte)AppearanceFlag1000.Elevated);
                w.Write((ushort)item.Flags.Height.Elevation);
            }

            if (item.Flags.LyingObject)
                w.Write((byte)AppearanceFlag1000.LyingObject);

            if (item.Flags.AnimateAlways)
                w.Write((byte)AppearanceFlag1000.AlwaysAnimated);

            if (item.Flags.Automap != null)
            {
                w.Write((byte)AppearanceFlag1000.MinimapColor);
                w.Write((ushort)item.Flags.Automap.Color);
            }

            if (item.Flags.Fullbank)
                w.Write((byte)AppearanceFlag1000.FullTile);

            if (item.Flags.Lenshelp != null)
            {
                w.Write((byte)AppearanceFlag1000.HelpInfo);
                w.Write((ushort)item.Flags.Lenshelp.Id);
            }

            if (item.Flags.IgnoreLook)
                w.Write((byte)AppearanceFlag1000.Lookthrough);

            if (item.Flags.Clothes != null)
            {
                w.Write((byte)AppearanceFlag1000.Clothes);
                w.Write((ushort)item.Flags.Clothes.Slot);
            }

            if (item.Flags.Market != null)
            {
                w.Write((byte)AppearanceFlag1000.Market);

                ushort category = 0;
                if ((ushort)item.Flags.Market.Category > 22)
                    category = 9;

                w.Write(category);
                w.Write((ushort)item.Flags.Market.TradeAsObjectId);
                w.Write((ushort)item.Flags.Market.ShowAsObjectId);
                w.Write((ushort)item.Name.Length);
                for (UInt16 i = 0; i < item.Name.Length; ++i)
                    w.Write((char)item.Name[i]);

                ushort Profession = 0;
                for (int i = 0; i < item.Flags.Market.RestrictToProfession.Count; ++i)
                    Profession += (ushort)item.Flags.Market.RestrictToProfession[i];
                w.Write(Profession);
                w.Write((ushort)item.Flags.Market.MinimumLevel);
            }

            if (item.Flags.DefaultAction != null)
            {
                w.Write((byte)AppearanceFlag1000.DefaultAction);
                w.Write((ushort)item.Flags.DefaultAction.Action);
            }

            if (item.Flags.Wrap)
                w.Write((byte)AppearanceFlag1000.Wrappable);

            if (item.Flags.Unwrap)
                w.Write((byte)AppearanceFlag1000.UnWrappable);

            if (item.Flags.Topeffect)
                w.Write((byte)AppearanceFlag1000.TopEffect);

            w.Write((byte)0xFF);

            if (type == 2)
                w.Write((byte)item.FrameGroup.Count);

            for (int i = 0; i < item.FrameGroup.Count; i++)
            {
                if (type == 2)
                    w.Write((byte)item.FrameGroup[i].FixedFrameGroup);


                uint Width = (uint)((item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].X + item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].Width) > 32 ? 2 : 1);
                uint Height = (uint)((item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].Y + item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].Height) > 32 ? 2 : 1);

                w.Write((byte)Width);
                w.Write((byte)Height);

                if (Width > 1 || Height > 1)
                {
                    uint ExactSize = Math.Max(item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].Width, item.FrameGroup[i].SpriteInfo.BoundingBoxPerDirection[0].Height);
                    w.Write((byte)ExactSize);
                }
                w.Write((byte)item.FrameGroup[i].SpriteInfo.Layers);
                w.Write((byte)item.FrameGroup[i].SpriteInfo.PatternWidth);
                w.Write((byte)item.FrameGroup[i].SpriteInfo.PatternHeight);
                w.Write((byte)item.FrameGroup[i].SpriteInfo.PatternDepth);

                if (item.FrameGroup[i].SpriteInfo.Animation != null)
                {
                    w.Write((byte)item.FrameGroup[i].SpriteInfo.Animation.SpritePhase.Count);
                    w.Write(Convert.ToByte(item.FrameGroup[i].SpriteInfo.Animation.Synchronized));
                    w.Write((uint)item.FrameGroup[i].SpriteInfo.Animation.LoopType);
                    if (item.FrameGroup[i].SpriteInfo.Animation.RandomStartPhase)
                        w.Write((byte)item.FrameGroup[i].SpriteInfo.Animation.DefaultStartPhase);
                    else
                        w.Write((byte)0x0);

                    for (int k = 0; k < item.FrameGroup[i].SpriteInfo.Animation.SpritePhase.Count; k++)
                    {
                        w.Write((uint)item.FrameGroup[i].SpriteInfo.Animation.SpritePhase[k].DurationMin);
                        w.Write((uint)item.FrameGroup[i].SpriteInfo.Animation.SpritePhase[k].DurationMax);
                    }
                }
                else
                    w.Write((byte)0x1);

                uint NumSprites = Width * Height * item.FrameGroup[i].SpriteInfo.Layers * item.FrameGroup[i].SpriteInfo.PatternWidth *
                    item.FrameGroup[i].SpriteInfo.PatternHeight * item.FrameGroup[i].SpriteInfo.PatternDepth;

                if (item.FrameGroup[i].SpriteInfo.Animation != null)
                    NumSprites = NumSprites * (uint)item.FrameGroup[i].SpriteInfo.Animation.SpritePhase.Count;

                uint sliceType = NumSprites / (uint)item.FrameGroup[i].SpriteInfo.SpriteId.Count;
                if (sliceType == 1)
                {
                    for (int j = 0; j < NumSprites; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                    }
                }
                else if (sliceType == 2)
                {
                    for (int j = 0; j < NumSprites/2; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 1));
                    }
                }
                else if (sliceType == 4)
                {
                    for (int j = 0; j < NumSprites/4; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 1));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 2));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 3));
                    }
                }

            }
            worker.ReportProgress((int)(progress * 100 / (ObjectCount + OutfitCount + EffectCount + MissileCount + catalog.Count)));

        }



    }
}






