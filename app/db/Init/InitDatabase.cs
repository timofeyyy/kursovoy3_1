using app.db.Context;
using app.db.Entities;
using app.db.Entities.Laptop;
using app.db.Entities.Laptop.VideoCard;
using app.db.Entities.OS;
using app.db.Entities.Processor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Color = app.db.Entities.Color;

namespace app.db.Init
{
    public class InitDatabase
    {   
        static readonly db.Context.Context context = new Context.Context();
        static List<string> producers = new List<string>()
            {
                "Lenovo",
                "ASUS",
                "HP",
                "Xiaomi",
                "Huawei",
                "Apple",
                "Samsung",
                "Nokia"
            };
        static List<(string OSName, List<string> Versions, bool isLaptop)> osBrands = new List<(string, List<string>, bool)>
        {
            ("Windows", new List<string> { "11", "10", "8.1", "8", "7", "Vista", "XP" }, true),
            ("Ubuntu", new List<string> { "23.10", "23.04", "22.10", "22.04", "20.04", "18.04", "16.04" }, true),
            ("Android", new List<string> { "14", "13", "12", "11", "10", "9", "8.1", "8.0" }, false),
            ("IOS", new List<string> { "17", "16", "15", "14", "13", "12", "11", "10" }, false),
            ("Blackberry OS", new List<string> { "10.3.3", "10.3.2", "10.3.1", "10.2.1", "10.2.0", "10.1", "10.0" }, false),
            ("Garuda Linux", new List<string> { "2023.09", "2023.08", "2023.07", "2023.06", "2023.05", "2023.04", "2023.03" }, true)
        };
        static List<(string brand, List<(string model, float baseFrequency, float boostFrequency)> models)> videoCards = new List<(string brand, List<(string model, float baseFrequency, float boostFrequency)> models)>()
        {
            (brand: "NVIDIA", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("GeForce RTX 4090", 2.23f, 2.52f),
                ("GeForce RTX 4080", 2.21f, 2.51f),
                ("GeForce RTX 4070 Ti", 2.31f, 2.61f),
                ("GeForce RTX 4060 Ti", 2.31f, 2.54f),
                ("GeForce RTX 3090 Ti", 1.56f, 1.86f)
            }),
            (brand: "AMD", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("Radeon RX 7900 XTX", 1.90f, 2.50f),
                ("Radeon RX 7900 XT", 1.50f, 2.40f),
                ("Radeon RX 7800 XT", 1.62f, 2.40f),
                ("Radeon RX 7700 XT", 1.80f, 2.45f),
                ("Radeon RX 6950 XT", 1.89f, 2.31f)
            }),
            (brand: "Intel", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("Intel Arc A770", 2.10f, 2.10f),
                ("Intel Arc A750", 2.05f, 2.05f),
                ("Intel Arc A580", 1.70f, 1.70f)
            }),
            (brand: "ASUS", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("ROG Strix RTX 4090 OC Edition", 2.23f, 2.64f),
                ("TUF Gaming RTX 4080", 2.21f, 2.58f),
                ("Dual RTX 4070", 2.31f, 2.54f),
                ("ROG Strix RX 7900 XTX", 1.90f, 2.60f),
                ("TUF Gaming RX 7800 XT", 1.62f, 2.50f)
            }),
            (brand: "MSI", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("GeForce RTX 4090 SUPRIM X", 2.23f, 2.61f),
                ("GeForce RTX 4080 Gaming Trio", 2.21f, 2.51f),
                ("Radeon RX 7900 XTX Gaming Trio", 1.90f, 2.55f),
                ("GeForce RTX 4070 Ti Ventus 3X", 2.31f, 2.67f),
                ("Radeon RX 7700 XT Mech 2X", 1.80f, 2.45f)
            }),
            (brand: "Gigabyte", models: new List<(string model, float baseFrequency, float boostFrequency)>()
            {
                ("AORUS RTX 4090 Xtreme Waterforce", 2.23f, 2.65f),
                ("RTX 4080 Gaming OC", 2.21f, 2.51f),
                ("Radeon RX 7900 XTX AORUS Elite", 1.90f, 2.55f),
                ("GeForce RTX 4070 Windforce", 2.31f, 2.54f)
            })
        };
        static List<(string brand, List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)> models)> processors = new List<(string brand, List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)> models)>()
        {
            (brand: "Intel", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("Intel Core i9-13900K", 3.0f, 5.8f, true),
                ("Intel Core i7-13700K", 3.4f, 5.4f, true)
            }),
            (brand: "AMD", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("AMD Ryzen 9 7950X", 4.5f, 5.7f, true),
                ("AMD Ryzen 7 7800X3D", 4.2f, 5.0f, true)
            }),
            (brand: "Apple", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("Apple M2 Max", 3.7f, 4.0f, true),
                ("Apple M1 Ultra", 3.2f, 3.2f, true),
                ("Apple A16 Bionic", 3.46f, 3.46f, false),
                ("Apple A15 Bionic", 3.23f, 3.23f, false),
                ("Apple A14 Bionic", 3.1f, 3.1f, false)
            }),
            (brand: "Qualcomm", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("Qualcomm Snapdragon 8 Gen 2", 2.84f, 3.2f, false),
                ("Qualcomm Snapdragon 8cx Gen 3", 2.4f, 3.0f, true),
                ("Qualcomm Snapdragon 865+", 3.1f, 3.1f, false),
                ("Qualcomm Snapdragon 888", 2.84f, 3.0f, false)
            }),
            (brand: "MediaTek", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("MediaTek Dimensity 9200", 3.0f, 3.05f, false),
                ("MediaTek Dimensity 8100", 2.85f, 2.85f, false),
                ("MediaTek Helio G99", 2.2f, 2.2f, false),
                ("MediaTek Dimensity 9000+", 3.2f, 3.35f, false)
            }),
            (brand: "Samsung", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("Samsung Exynos 2200", 2.8f, 2.8f, false),
                ("Samsung Exynos 2100", 2.9f, 3.0f, false)
            }),
            (brand: "Google", models: new List<(string model, float baseFrequency, float boostFrequency, bool isLaptop)>()
            {
                ("Google Tensor G3", 2.91f, 3.0f, false),
                ("Google Tensor G2", 2.85f, 2.85f, false)
            })
        };



        static public void Init()
        {
            if(context.Producer.Count() == 0)
                foreach (var producer in producers)
                    if (context.Producer.Where(p => p.Name.ToLower().Equals(producer.ToLower())).Any() != null)
                        context.Producer.Add(new Producer()
                        {
                            Name = producer
                        });
           
            if(context.OSBrand.Count() == 0)
            foreach (var brand in osBrands)
                if (context.OSBrand.Where(p => p.Name.Equals(brand)).Any() != null)
                    {
                        OSBrand osbrand = new OSBrand()
                        {
                            Name = brand.OSName,
                            IsLaptop = brand.isLaptop
                        };
                        context.OSBrand.Add(osbrand);
                        context.SaveChanges();
                        foreach (var version in brand.Versions)
                        {
                            context.OS.Add(new OS()
                            {
                                //Brand = osbrand,
                                BrandId = osbrand.Id,
                                Version = version
                            });
                        }

                    }

            if (context.Color.Count() == 0)
                foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties())
                    if (property.PropertyType == typeof(System.Drawing.Color))
                    {
                        System.Drawing.Color color = (System.Drawing.Color)property.GetValue(null);
                        string value = color.Name;

                        if (context.Color.Where(p => p.Value.ToLower().Equals(value.ToLower())).Any() != null)
                            context.Add(new Color()
                            {
                                Value = value.ToLower()
                            });
                    }

            if (context.VideoCardBrand.Count() == 0)
                foreach (var brand in videoCards)
                    if (context.VideoCardBrand.Where(vb => vb.Name.ToLower().Equals(brand.brand.ToLower())).Any() != null)
                    {
                        VideoCardBrand videoCardBrand = new VideoCardBrand()
                        {
                            Name = brand.brand
                        };
                        context.VideoCardBrand.Add(videoCardBrand);
                        context.SaveChanges();
                        foreach (var model in brand.models)
                            context.VideoCardModel.Add(new VideoCardModel
                            {
                                BrandId = videoCardBrand.Id,
                                //Brand = videoCardBrand,
                                Name = model.model,
                                BaseFrequency = model.baseFrequency,
                                BoostFrequency = model.boostFrequency
                            });
                    }

            if (context.ProcessorBrand.Count() == 0)
                foreach (var brand in processors)
                    if (context.ProcessorBrand.Where(pb => pb.Name.ToLower().Equals(brand.brand.ToLower())).Any() != null)
                    {
                        ProcessorBrand processorBrand = new ProcessorBrand()
                        {
                            Name = brand.brand
                        };
                        context.ProcessorBrand.Add(processorBrand);
                        context.SaveChanges();
                        foreach (var model in brand.models)
                            context.ProcessorModel.Add(new ProcessorModel
                            {
                                BrandId = processorBrand.Id,
                                //Brand = processorBrand,
                                IsLaptop = model.isLaptop,
                                Name = model.model,
                                BaseFrequency = model.baseFrequency,
                                BoostFrequency = model.boostFrequency
                            });
                    }

            context.SaveChanges();
        }
    }
}
