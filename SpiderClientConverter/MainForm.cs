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
using System.Collections.Concurrent;

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
        public OpenTibia.Core.Version version;
        public OpenTibia.Client.ClientFeatures Features;
        public SpriteStorage sprites;
        public int progress;

        public uint DatSignature { get; set; }
        public uint SprSignature { get; set; }
        public ushort ObjectCount { get; set; }
        public ushort OutfitCount { get; set; }
        public ushort EffectCount { get; set; }
        public ushort MissileCount { get; set; }
        public List<int> SpritesOffset { get; set; }
        public ConcurrentDictionary<int, Sprite[]> concurrentDictionary = new ConcurrentDictionary<int, Sprite[]>();
        public SpiderClientConverter()
        {
            InitializeComponent();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_Completed;
            DatSignature = 0x4A10;
            SprSignature = 0x59E48E02;
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

            if (ToSpr.Checked)
            {
                version = new OpenTibia.Core.Version(1000, "Client 10.00", DatSignature, SprSignature, 0);
                Features = OpenTibia.Client.ClientFeatures.Extended;
                sprites = SpriteStorage.Create(version, Features);
            }

            progress = 0;
            Directory.CreateDirectory(_dumpToPath + @"//slices//");
            Parallel.ForEach(catalog, options, (sheet, state) =>
            {
                progress++;
                if (sheet.Type == "sprite")
                    DumpSpriteSheet(sheet);

                worker.ReportProgress((int)(progress * 100 / catalog.Count));
            });

            if (ToSpr.Checked)
            {
                foreach (var tile in concurrentDictionary.OrderBy(tile => tile.Key))
                {
                    sprites.AddSprites(tile.Value);
                }

                sprites.Save(String.Format("{0}//Clients//Tibia.spr", _dumpToPath), version);
            }
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
                    w.Write(DatSignature);
                    w.Write(ObjectCount);
                    w.Write(OutfitCount);
                    w.Write(EffectCount);
                    w.Write(MissileCount);
                    int CurrentId = 0;
                    for (int i = 0; i <= appearances.Object[appearances.Object.Count - 1].Id - 100; i++)
                    {
                        if (i + 100 == appearances.Object[CurrentId].Id && appearances.Object[CurrentId].FrameGroup.Count() > 0)
                        {
                            WriteAppearance1000(w, appearances.Object[CurrentId], 1);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 };
                            w.Write(buffer);
                        }
                    }
                    CurrentId = 0;
                    for (int i = 1; i <= appearances.Outfit[appearances.Outfit.Count - 1].Id; i++)
                    {
                        if (appearances.Outfit[CurrentId] != null && i == appearances.Outfit[CurrentId].Id && appearances.Object[CurrentId].FrameGroup.Count() > 0)
                        {
                            WriteAppearance1000(w, appearances.Outfit[CurrentId], 2);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 };
                            w.Write(buffer);
                        }
                    }
                    CurrentId = 0;
                    for (int i = 1; i <= appearances.Effect[appearances.Effect.Count - 1].Id; i++)
                    {
                        if (appearances.Effect[CurrentId] != null && i == appearances.Effect[CurrentId].Id && appearances.Object[CurrentId].FrameGroup.Count() > 0)
                        {
                            WriteAppearance1000(w, appearances.Effect[CurrentId], 3);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 };
                            w.Write(buffer);
                        }
                    }
                    CurrentId = 0;
                    for (int i = 1; i <= appearances.Missile[appearances.Missile.Count - 1].Id; i++)
                    {
                        if (appearances.Missile[CurrentId] != null && i == appearances.Missile[CurrentId].Id && appearances.Object[CurrentId].FrameGroup.Count() > 0)
                        {
                            WriteAppearance1000(w, appearances.Missile[CurrentId], 4);
                            CurrentId++;
                        }
                        else
                        {
                            byte[] buffer = { 0xFF, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00 };
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
            if (ExSheets.Checked)
                image.Save(String.Format("{0}Sprites {1}-{2}-{3}.png", _dumpToPath, sheet.FirstSpriteid.ToString(), sheet.LastSpriteid.ToString(), sheet.SpriteType.ToString()), ImageFormat.Png);

            if (ToSpr.Checked || SliceBox.Checked)
            {
                Size tileSize = new Size(32, 32);
                GenerateTileSetImageList(image, tileSize, sheet);
            }
        }

        public string GetSprName(int Id, int tileId, int spriteType)
        {
            string sprName = "";
            if (spriteType <= 2)
                sprName = (Id + tileId).ToString();
            else if (spriteType <= 2)
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

                sprID = (tileId / 4.0) - Math.Truncate((double)tileId / 4.0);
                if (sprID == 0.0)
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

        public int GetSheetType(uint sprId)
        {
            foreach (Catalog sheet in catalog)
            {
                if (sheet.FirstSpriteid <= sprId && sheet.LastSpriteid >= sprId)
                    return sheet.SpriteType;
            }

            return 0;
        }

        public void GenerateTileSetImageList(Image tileSetImage, Size tileSize, Catalog sheet)
        {
            try
            {
                int tileCount = sheet.LastSpriteid - sheet.FirstSpriteid;
                if (sheet.SpriteType == 3)
                    tileCount = (tileCount * 4) + 4;
                else if (sheet.SpriteType >= 1)
                    tileCount = (tileCount * 2) + 2;
                else
                    tileCount += 1;

                Sprite[] Tiles = new Sprite[tileCount];
                int sprCount = 0;

                Image tile = default(Image);
                int xCounter = 0, yCounter = 0;
                if (sheet.SpriteType == 0)
                {
                    for (int x = 0; x <= 11; x++)
                    {
                        for (int y = 0; y <= 11; y++)
                        {
                            if (sprCount >= tileCount)
                                break;

                            tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                            Graphics g = Graphics.FromImage(tile);
                            Rectangle sourceRect = new Rectangle(y * tileSize.Width, x * tileSize.Height, tileSize.Width, tileSize.Height);
                            g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                            Sprite item = new Sprite();
                            item.SetBitmap((Bitmap)tile);
                            Tiles[sprCount] = item;
                            if (SliceBox.Checked)
                                    tile.Save(_dumpToPath + @"//slices//" + GetSprName(sheet.FirstSpriteid, sprCount, sheet.SpriteType) + ".png");
                            g.Dispose();
                            tile.Dispose();
                            sprCount++;
                        }
                    }
                }
                else if (sheet.SpriteType == 1)
                {
                    
                    for (int x = 0; x <= 5; x++)
                    {
                        for (int y = 0; y <= 11; y++)
                        {
                            for (int a = 0; a <= 1; a++)
                            {
                                if (sprCount >= tileCount)
                                    break;
                                tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                Graphics g = Graphics.FromImage(tile);
                                Rectangle sourceRect = new Rectangle(y * tileSize.Width, (x +  xCounter + a) * tileSize.Height, tileSize.Width, tileSize.Height);
                                g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                Sprite item = new Sprite();
                                item.SetBitmap((Bitmap)tile);
                                Tiles[sprCount] = item;
                                if (SliceBox.Checked)
                                        tile.Save(_dumpToPath + @"//slices//" + GetSprName(sheet.FirstSpriteid, sprCount, sheet.SpriteType) + ".png");
                                g.Dispose();
                                tile.Dispose();
                                sprCount++;
                            }
                        }
                        xCounter = xCounter + 1;
                    }
                }
                else if (sheet.SpriteType == 2)
                {
                    for (int x = 0; x <= 11; x++)
                    {
                        for (int y = 0; y <= 5; y++)
                        {
                            for (int a = 0; a <= 1; a++)
                            {
                                if (sprCount >= tileCount)
                                    break;

                                tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                Graphics g = Graphics.FromImage(tile);
                                Rectangle sourceRect = new Rectangle((y + xCounter + a) * tileSize.Height, x * tileSize.Width, tileSize.Width, tileSize.Height);
                                g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                Sprite item = new Sprite();
                                item.SetBitmap((Bitmap)tile);
                                Tiles[sprCount] = item;
                                if (SliceBox.Checked)
                                        tile.Save(_dumpToPath + @"//slices//" + GetSprName(sheet.FirstSpriteid, sprCount, sheet.SpriteType) + ".png");
                                
                                g.Dispose();
                                tile.Dispose();
                                sprCount++;
                            }
                            xCounter = xCounter + 1;
                            
                        }
                        xCounter = 0;
                    }
                }
                else if (sheet.SpriteType == 3)
                {
                    for (int x = 0; x <= 5; x++)
                    {
                        for (int y = 0; y <= 5; y++)
                        {
                            for (int a = 0; a <= 1; a++)
                            {
                                for (int b = 0; b <= 1; b++)
                                {
                                    if (sprCount >= tileCount)
                                        break;

                                    tile = new Bitmap(tileSize.Width, tileSize.Height, tileSetImage.PixelFormat);
                                    Graphics g = Graphics.FromImage(tile);
                                    Rectangle sourceRect = new Rectangle((y + b + xCounter) * tileSize.Width, (x + a + yCounter) * tileSize.Height, tileSize.Width, tileSize.Height);
                                    g.DrawImage(tileSetImage, new Rectangle(0, 0, tileSize.Width, tileSize.Height), sourceRect, GraphicsUnit.Pixel);
                                    Sprite item = new Sprite();
                                    item.SetBitmap((Bitmap)tile);
                                    Tiles[sprCount] = item;
                                    if (SliceBox.Checked)
                                            tile.Save(_dumpToPath + @"//slices//" + GetSprName(sheet.FirstSpriteid, sprCount, sheet.SpriteType) + ".png");
                                    g.Dispose();
                                    tile.Dispose();
                                    sprCount++;
                                }
                            }
                                    
                            if (xCounter < 5)
                                xCounter = xCounter + 1;
                            else
                            {
                                xCounter = 0;
                                yCounter = yCounter + 1;
                            }                                    
                        }
                    }
                }
                concurrentDictionary.TryAdd(sheet.FirstSpriteid, Tiles);
                tileSetImage.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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

                uint Width = 1;
                uint Height = 1;
                int sliceType = GetSheetType(item.FrameGroup[i].SpriteInfo.SpriteId[0]);

                if (sliceType == 1)
                {
                    Width = 1;
                    Height = 2;
                }
                else if (sliceType == 2)
                {
                    Width = 2;
                    Height = 1;
                }
                else if (sliceType == 3)
                {
                    Width = 2;
                    Height = 2;
                }


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

                if (sliceType == 0)
                {
                    for (int j = 0; j < NumSprites; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                    }
                }
                else if (sliceType == 1 ||sliceType == 2)
                {
                    for (int j = 0; j < NumSprites/2; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 1));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                    }
                }
                else if (sliceType == 3)
                {
                    for (int j = 0; j < NumSprites/4; j++)
                    {
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 3));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 2));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]] + 1));
                        w.Write((uint)(SpritesOffset[(int)item.FrameGroup[i].SpriteInfo.SpriteId[j]]));
                    }
                }

            }
            worker.ReportProgress((int)(progress * 100 / (ObjectCount + OutfitCount + EffectCount + MissileCount + catalog.Count)));

        }

        private void CustomSiganture_CheckedChanged(object sender, EventArgs e)
        {
            sigPanel.Visible = CustomSiganture.Checked;
            if (CustomSiganture.Checked == false)
            {
                DatSignature = 0x4A10;
                SprSignature = 0x59E48E02;
            }
            datHex.Value = DatSignature;
            sprHex.Value = SprSignature;
        }

        private void DatHex_ValueChanged(object sender, EventArgs e)
        {
            DatSignature = (uint)datHex.Value;
        }

        private void SprHex_ValueChanged(object sender, EventArgs e)
        {
            SprSignature = (uint)sprHex.Value;
        }
    }
}






