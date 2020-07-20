using DataAccess.Models.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Models.Repository
{
   public class AbstractCreateRepository : IInstantCreate
        
    {
        private readonly BugTriageContext context;

        public AbstractCreateRepository(BugTriageContext context)
        {
            this.context = context;
        }
        public int CreateInsatntProductProfile(string name)
        {
            if (name == null || name.Length == 0) return 0;
            Product product = context.Products.FirstOrDefault(x => x.ProductName.ToLower() == name.ToLower());
            if (product == null)
            {
                product = new Product { ProductName = name };
                context.Products.Add(product);
                context.SaveChanges();
            }

            return product.ProductId;
        }

        public int CreateInsatntComponentProfile(string name)
        {
            if (name == null || name.Length == 0) return 0;
            Component component = context.Components.FirstOrDefault(x => x.ComponentName.ToLower() == name.ToLower());
            if (component == null)
            {
                component = new Component { ComponentName = name };
                context.Components.Add(component);
                context.SaveChanges();
            }

            return component.ComponentId;
        }


        public int CreateInsatntHardwareProfile(string name)
        {
            if (name == null || name.Length == 0) return 0;
            Hardware hardware = context.Hardwares.FirstOrDefault(x => x.hardware_name.ToLower() == name.ToLower());
            if (hardware == null)
            {
                hardware = new Hardware { hardware_name = name };
                context.Hardwares.Add(hardware);
                context.SaveChanges();
            }

            return hardware.hardware_id;
        }
        public int CreateInsatntDeveloperProfile(string name, int developerTypeId=0)
        {
            if (name == null || name.Length == 0) return 0;
            Developer developer = context.Developer.FirstOrDefault(x => x.DeveloperName.ToLower().Trim() == name.ToLower().Trim());
            if (developer == null)
            {
                developer = new Developer
                {
                    DeveloperName = name,
                    DeveloperTypeId = (developerTypeId==0?1:developerTypeId)
                };
                context.Developer.Add(developer);
                context.SaveChanges();
            }

            return developer.DeveloperId;
        }
    }
}
