using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace csharpbasics_lab3
{
    #region BuilderPattern
    class SetBuilder //Director
    {
        Builder builder;
        public SetBuilder(Builder builder)
        {
            this.builder = builder;
        }
        public void Construct()
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();
        }
    }
    
    public class SushiSet //Product
    {
        public ObservableCollection<object> parts { get; set; }
        
        public string name { get; set; }
        public SushiSet(string name)
        {
            this.name = name;
            parts = new ObservableCollection<object>();
        }
        public void AddSauce(string part)
        {
            parts.Add(part);
        }
    }

    abstract class Builder //Builder
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void BuildPartC();
        public abstract SushiSet GetSushi();
    }
    #endregion
    //======================================================================
    #region SushiAndSaucesDefinition

    #endregion
    //======================================================================
    #region BasicSushiSets
    class States : Builder
    {
        SushiSet product = new SushiSet("Штаты");
        public override void BuildPartA()
        {
            product.AddSauce("Сырный соус");
        }
        public override void BuildPartB()
        {
            product.AddSauce("Соевый соус");
        }
        public override void BuildPartC()
        {
            product.AddSauce("Кисло-сладкий соус");
        }
        public override SushiSet GetSushi()
        {
            return product;
        }
    }

    class Glory : Builder
    {
        SushiSet product = new SushiSet("Великолепие");
        public override void BuildPartA()
        {
            product.AddSauce("Чесночный соус");
        }
        public override void BuildPartB()
        {
            product.AddSauce("Кетчуп");
        }
        public override void BuildPartC()
        {
            
        }
        public override SushiSet GetSushi()
        {
            return product;
        }
    }

    class Autumn : Builder
    {
        SushiSet product = new SushiSet("Осень");
        public override void BuildPartA()
        {
            product.AddSauce("Соевый соус");
        }
        public override void BuildPartB()
        {
            product.AddSauce("Лимонный сок");
        }
        public override void BuildPartC()
        {
            product.AddSauce("Кисло-сладкий соус");
        }
        public override SushiSet GetSushi()
        {
            return product;
        }
    }
    #endregion
}
