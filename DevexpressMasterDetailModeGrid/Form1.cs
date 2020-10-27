using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevexpressMasterDetailModeGrid.Entities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevexpressMasterDetailModeGrid
{
    public partial class Form1 : Form
    {
        public static string AccessGroupsLevelName = "UserSetting";
        private GridView detailPatternView = new GridView { ViewCaption = AccessGroupsLevelName };
        private int _selectedAccessGroupID;
        private int rowHandle;
        private GridColumn column;

        public Form1()
        {
            InitializeComponent();
            detailPatternView.PopupMenuShowing += DetailPatternView_PopupMenuShowing;
        }

        private void DetailPatternView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _selectedAccessGroupID = Convert.ToInt32((sender as GridView).GetFocusedRowCellValue("Id").ToString());

            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            popupMenu1.ItemLinks.Clear();
            barManager1.ForceInitialize();
            popupMenu1.ItemLinks.Add(new BarButtonItem(barManager1, "Düzenle"));
            popupMenu1.ItemLinks.Add(new BarButtonItem(barManager1, "Sil"));
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = rowHandle = hitInfo.RowHandle;
                column = hitInfo.Column;
                detailPatternView.SelectRow(rowHandle);
                popupMenu1.ShowPopup(barManager1, view.GridControl.PointToScreen(e.Point));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = GetData();
            // Create a new detail pattern view
            detailPatternView.Appearance.Row.BackColor = Color.Yellow;
            detailPatternView.Appearance.Row.Options.UseBackColor = true;

            gridControl1.ViewCollection.Add(detailPatternView);
            gridControl1.LevelTree.Nodes.Add(AccessGroupsLevelName, detailPatternView);
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsView.ShowIndicator = false;

            // gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.BestFitColumns();

            detailPatternView.OptionsView.ShowIndicator = false;
            detailPatternView.OptionsBehavior.Editable = false;
            detailPatternView.OptionsSelection.MultiSelect = true;
            detailPatternView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            detailPatternView.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            detailPatternView.BestFitColumns();
            // Associate the detailPatternView view with the Products level
        }

        public BindingList<UserSettingDto> GetData()
        {
            BindingList<UserSettingDto> newList = new BindingList<UserSettingDto>();
            newList.Add(new UserSettingDto()
            {
                Id = 1,
                CardID = 1040664890,
                FirstName = "Ali Yasin",
                LastName = "DOĞAN",
                UserSettings = new BindingList<UserSetting> {
                    new UserSetting { Id = 3, FullName = "Ali Yasin DOĞAN", DeviceName = "Mw-301 YENI"} ,
                    new UserSetting { Id = 4, FullName = "Ali Yasin DOĞAN", DeviceName = "Mw301 ESKI"} ,
                }
            });
            newList.Add(new UserSettingDto()
            {
                Id = 2,
                CardID = 974228247,
                FirstName = "İlkay Burak",
                LastName = "UZUNALİ",
                UserSettings = new BindingList<UserSetting> {
                    new UserSetting { Id = 4, FullName = "İlkay Burak UZUNALİ", DeviceName = "Mw301 ESKI"} ,
                    new UserSetting { Id = 5, FullName = "İlkay Burak UZUNALİ", DeviceName = "Mw-301 YENI"} ,
                }
            });
            newList.Add(new UserSettingDto()
            {
                Id = 3,
                CardID = 52063944,
                FirstName = "Barış",
                LastName = "ALBAYRAK",
                UserSettings = new BindingList<UserSetting> {
                    new UserSetting { Id = 5, FullName = "Barış ALBAYRAK", DeviceName = "Mw-301 YENI"} ,
                }
            });
            newList.Add(new UserSettingDto()
            {
                Id = 4,
                CardID = 44175396,
                FirstName = "Hakan",
                LastName = "AKTEPE",
                UserSettings = new BindingList<UserSetting> {
                    new UserSetting { Id = 3, FullName = "Hakan AKTEPE", DeviceName = "Mw301 ESKI"} ,
                    new UserSetting { Id = 4, FullName = "Hakan AKTEPE", DeviceName = "Mw-301 YENI"} ,
                }
            });
            return newList;
        }

        #region OldCode

        //public class Category
        //{
        //    public int ID { get; set; }
        //    public string CategoryName { get; set; }
        //    public string Description { get; set; }
        //    public BindingList<Product> Products { get; set; }

        //    public static BindingList<Category> GetMasterDetailData()
        //    {
        //        BindingList<Category> list = new BindingList<Category>();
        //        list.Add(new Category() { ID = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales", Products = new BindingList<Product> { new Product() { ProductName = "Guaraná Fantástica", CategoryID = 1, UnitPrice = 4.5m } } });
        //        list.Add(new Category() { ID = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings", Products = new BindingList<Product> { new Product() { ProductName = "Sir Corey's Scones", CategoryID = 2, UnitPrice = 110 }, new Product() { ProductName = "Sir Joes's Scones", CategoryID = 2, UnitPrice = 231.23m }, new Product() { ProductName = "Amanda's Scones", CategoryID = 2, UnitPrice = 31.23m } } });
        //        list.Add(new Category() { ID = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads", Products = new BindingList<Product> { new Product() { ProductName = "Sir Rodney's Scones", CategoryID = 3, UnitPrice = 10 }, new Product() { ProductName = "Gumbär Gummibärchen", CategoryID = 3, UnitPrice = 31.23m }, new Product() { ProductName = "NuNuCa Nuß-Nougat-Creme", CategoryID = 3, UnitPrice = 14 }, new Product() { ProductName = "Gumbär", CategoryID = 3, UnitPrice = 331.23m }, new Product() { ProductName = "Gummibärchen", CategoryID = 3, UnitPrice = 321.23m } } });
        //        list.Add(new Category() { ID = 4, CategoryName = "Dairy Products", Description = "Cheeses", Products = new BindingList<Product> { new Product() { ProductName = "Gorgonzola Telino", CategoryID = 4, UnitPrice = 12.5m }, new Product() { ProductName = "Gorgonzola", CategoryID = 4, UnitPrice = 112.5m }, new Product() { ProductName = "Telino", CategoryID = 4, UnitPrice = 122.5m } } });
        //        list.Add(new Category() { ID = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal", Products = new BindingList<Product> { new Product() { ProductName = "Tunnbröd", CategoryID = 5, UnitPrice = 9 }, new Product() { ProductName = "Gustaf's Knäckebröd", CategoryID = 5, UnitPrice = 21 }, new Product() { ProductName = "Knäckebröd", CategoryID = 5, UnitPrice = 221 } } });
        //        list.Add(new Category() { ID = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats", Products = new BindingList<Product> { new Product() { ProductName = "Thüringer Rostbratwurst", CategoryID = 6, UnitPrice = 123.79m }, new Product() { ProductName = "Thüringer", CategoryID = 6, UnitPrice = 223.79m }, new Product() { ProductName = "Rostbratwurst", CategoryID = 6, UnitPrice = 133.79m } } });
        //        list.Add(new Category() { ID = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd", Products = new BindingList<Product> { new Product() { ProductName = "Rössle Sauerkraut", CategoryID = 7, UnitPrice = 45.6m }, new Product() { ProductName = "Rössle", CategoryID = 7, UnitPrice = 55.6m }, new Product() { ProductName = "Sauerkraut", CategoryID = 7, UnitPrice = 35.6m } } });
        //        list.Add(new Category() { ID = 8, CategoryName = "Seafood", Description = "Seaweed and fish", Products = new BindingList<Product> { new Product() { ProductName = "Nord-Ost Matjeshering", CategoryID = 8, UnitPrice = 25.89m }, new Product() { ProductName = "Nord-Ost", CategoryID = 8, UnitPrice = 23.89m }, new Product() { ProductName = "Nord Matjeshering", CategoryID = 8, UnitPrice = 29.89m } } });
        //        return list;
        //    }

        //    public static string ProductsLevelName = "Products";
        //}

        //public class Product
        //{
        //    public string ProductName { get; set; }
        //    public decimal UnitPrice { get; set; }
        //    public int CategoryID { get; set; }
        //}

        #endregion OldCode

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Caption == "Düzenle")
            {
                MessageBox.Show("Geçiş Grubu ID:" + _selectedAccessGroupID);
            }
        }
    }
}