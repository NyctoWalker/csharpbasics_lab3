using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace csharpbasics_lab3
{
    public class AppVM : INotifyPropertyChanged
    {
        #region Variables&Collections
        public ObservableCollection<object> setsOfSushi { get; private set; }
        public ObservableCollection<object> basicsauces { get; set; }
        public ObservableCollection<string> saucesMenu { get; private set; }
        public ObservableCollection<object> addsauces { get; set; }
        private int bSauceCount = 1;
        private int aSauceCount = 1;

        private SushiSet selectedSushi;
        private string selectedMenu;
        private string selectedAdded;
        public SushiSet SelectedSushi
        {
            get { return selectedSushi; }
            set
            {
                selectedSushi = value;
                OnPropertyChanged("SelectedSushi");
            }
        }
        public string SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                selectedMenu = value;
                OnPropertyChanged("SelectedMenu");
            }
        }
        public string SelectedAdded
        {
            get { return selectedAdded; }
            set
            {
                selectedAdded = value;
                OnPropertyChanged("SelectedAdded");
            }
        }

        public int BSauceCount
        {
            get { return bSauceCount; }
            set
            {
                bSauceCount = value;
                OnPropertyChanged("BSauceCount");
            }
        }
        public int ASauceCount
        {
            get { return aSauceCount; }
            set
            {
                aSauceCount = value;
                OnPropertyChanged("ASauceCount");
            }
        }
        #endregion

        #region Constructor
        public AppVM()
        {
            setsOfSushi = new ObservableCollection<object> { };
            basicsauces = new ObservableCollection<object> { };
            addsauces = new ObservableCollection<object> { };
            saucesMenu = new ObservableCollection<string> {"Сырный соус", "Соевый соус", "Чесночный соус", "Кетчуп", "Кисло-сладкий соус", "Лимонный сок" };
            SelectedMenu = saucesMenu[0];

            Builder builder = new States();
            SetBuilder sushiset = new SetBuilder(builder);
            sushiset.Construct();
            setsOfSushi.Add(builder.GetSushi());

            builder = new Glory();
            sushiset = new SetBuilder(builder);
            sushiset.Construct();
            setsOfSushi.Add(builder.GetSushi());

            builder = new Autumn();
            sushiset = new SetBuilder(builder);
            sushiset.Construct();
            setsOfSushi.Add(builder.GetSushi());
        }
        #endregion

        #region Property

        #endregion

        #region Commands
        private RelayCommand addSauce;
        private RelayCommand removeSauce;
        private RelayCommand saveOrder;
        public RelayCommand AddSauce
        {
            get
            {
                return addSauce ??
                    (addSauce = new RelayCommand(obj =>
                    {
                        if (ASauceCount > 0 && ASauceCount < 11)
                        {
                            addsauces.Add(selectedMenu + " " + ASauceCount + "шт.");
                            SelectedMenu = saucesMenu[0];
                            ASauceCount = 1;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Введите действительное число от 1 до 10", "Ошибка добавления");
                        }
                    }));
            }
        }
        public RelayCommand RemoveSauce
        {
            get
            {
                return removeSauce ??
                    (removeSauce = new RelayCommand(obj =>
                    {
                        if (SelectedAdded != null)
                        {
                            addsauces.Remove(SelectedAdded);
                        }
                        else
                            System.Windows.MessageBox.Show("Сначала выберите соусы(-ы) для удаления", "Ошибка удаления");
                    }));
            }
        }
        public RelayCommand SaveOrder 
        {
            get
            {
                return saveOrder ??
                    (saveOrder = new RelayCommand(obj =>
                    {
                    if (BSauceCount > 0 && BSauceCount < 11 && !(SelectedSushi==null))
                    {
                        string filePath = @"C:\Users\Yulaw\Desktop\order.txt";

                        using (StreamWriter writer = File.AppendText(filePath))
                            {
                                writer.WriteLine("[Заказ]");
                                writer.WriteLine($"Базовый набор: {SelectedSushi.name}");
                                writer.WriteLine($"Базовых соусов({BSauceCount} шт. каждого):");
                                for (int i = 0; i < SelectedSushi.parts.Count; i++)
                                    writer.WriteLine($"{SelectedSushi.parts[i]}");
                                writer.WriteLine("Дополнительные соусы: ");
                                if (addsauces.Count != 0)
                                {
                                    for (int i = 0; i < addsauces.Count; i++)
                                        writer.WriteLine($"{addsauces[i]}");
                                }
                                else
                                    writer.WriteLine("Нет.");
                                writer.WriteLine("");
                            }    
                        
                        addsauces.Clear();
                        }
                        else
                        { System.Windows.MessageBox.Show("Введите корректное количество соусов базового набора и выберите базовый набор", "Ошибка оформления"); }
                    }));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
