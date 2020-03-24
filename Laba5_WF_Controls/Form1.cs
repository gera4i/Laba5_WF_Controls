using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba5_WF_Controls
{
    public partial class ConstructorOfFlat : Form
    {
        public ConstructorOfFlat()
        {
            InitializeComponent();
            UpdateList();
            numOfWindows.Text = String.Empty;
            trackBar1.Scroll += trackBar1_Scroll;
        }

        List <Room> rooms = new List<Room>();
        private void addRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AreaOfRoom.Text))
            {
                MessageBox.Show("Вы не указали площадь комнаты!");
                return;
            }
            if (string.IsNullOrWhiteSpace(numOfWindows.Text))
            {
                MessageBox.Show("Вы не указали количество окон!");
                return;
            }
            if (string.IsNullOrWhiteSpace(sideOfWindows.Text))
            {
                MessageBox.Show("Вы не указали направление окон!");
                return;
            }
            Room room = new Room();
            room.area = Convert.ToDouble(AreaOfRoom.Text);
            room.numberOfWindows = Convert.ToInt32(numOfWindows.Value);
            switch(sideOfWindows.SelectedIndex)
            {
                case 0:
                    room.sideOfWindows = Side.North;
                    break;
                case 1:
                    room.sideOfWindows = Side.South;
                    break;
                case 2:
                    room.sideOfWindows = Side.West;
                    break;
                case 3:
                    room.sideOfWindows = Side.East;
                    break;
                default: break;
            }
            rooms.Add(room);
            MessageBox.Show("Комната добавлена.");
            AreaOfRoom.Clear();
            numOfWindows.Text = String.Empty;
            sideOfWindows.Text = "";
        }

        private void addFlat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(country.Text))
            {
                MessageBox.Show("Вы не указали страну!");
                return;
            }
            if (string.IsNullOrWhiteSpace(town.Text))
            {
                MessageBox.Show("Вы не указали город!");
                return;
            }
            if (string.IsNullOrWhiteSpace(district.Text))
            {
                MessageBox.Show("Вы не указали район!");
                return;
            }
            if (string.IsNullOrWhiteSpace(street.Text))
            {
                MessageBox.Show("Вы не указали улицу!");
                return;
            }
            if (string.IsNullOrWhiteSpace(numberOfBuilding.Text))
            {
                MessageBox.Show("Вы не указали номер дома!");
                return;
            }
            if (string.IsNullOrWhiteSpace(numberOfFlat.Text))
            {
                MessageBox.Show("Вы не указали номер квартиры!");
                return;
            }
            if (string.IsNullOrWhiteSpace(Area.Text))
            {
                MessageBox.Show("Вы не указали площадь квартиры!");
                return;
            }
            if (numOfRooms1.Checked == false && numOfRooms2.Checked == false && numOfRooms3.Checked == false && numOfRooms4.Checked == false)
            {
                MessageBox.Show("Вы не указали количество комнат!");
                return;
            }
            if (string.IsNullOrWhiteSpace(material.Text))
            {
                MessageBox.Show("Вы не указали материал!");
                return;
            }
          
            if (string.IsNullOrWhiteSpace(level.Text))
            {
                MessageBox.Show("Вы не указали этаж!");
                return;
            }
            if (rooms.Count == 0)
            {
                MessageBox.Show("Добавьте сначала комнату(ы)!");
                return;
            }

            Flat flat = new Flat();
            Adress adress = new Adress();
            adress.country = Convert.ToString(country.Text);
            adress.town = Convert.ToString(town.Text);
            adress.district = Convert.ToString(district.Text);
            adress.street = Convert.ToString(street.Text);
            adress.numberOfBuilding = Convert.ToInt32(numberOfBuilding.Text);
            if (string.IsNullOrWhiteSpace(subNumber.Text))
            {
                adress.subNumberOfBuilding = null;
            }
            else
            {
                adress.subNumberOfBuilding = Convert.ToInt32(subNumber.Text);
            }
            adress.numberOfFlat = Convert.ToInt32(numberOfFlat.Text);
            flat.adress = adress;
            flat.size = Convert.ToInt32(Area.Text);
            if (numOfRooms1.Checked)
            {
                flat.numOfRooms = 1;
            }
            if (numOfRooms2.Checked)
            {
                flat.numOfRooms = 2;
            }
            if (numOfRooms3.Checked)
            {
                flat.numOfRooms = 3;
            }
            if (numOfRooms4.Checked)
            {
                flat.numOfRooms = 4;
            }


            if (options.GetItemChecked(0))
            {
                flat.options.Add(Option.Studio);
            }
            if (options.GetItemChecked(1))
            {
                flat.options.Add(Option.SeparatedWC);
            }
            if (options.GetItemChecked(2))
            {
                flat.options.Add(Option.Balkon);
            }
            if (options.GetItemChecked(3))
            {
                flat.options.Add(Option.Podval);
            }
            switch (sideOfWindows.SelectedIndex)
            {
                case 0:
                    flat.typeOfMaterial = Material.Brick;
                    break;
                case 1:
                    flat.typeOfMaterial = Material.Panel;
                    break;
                case 2:
                    flat.typeOfMaterial = Material.Wood;
                    break;
                default: break;
            }
            flat.year = Convert.ToInt32(yearPicker1.Value.Year);
           flat.level = Convert.ToInt32(level.Text);
            flat.rooms = rooms;
            FlatModel.shared.flats.Add(flat);
            MessageBox.Show("Квартира добавлена.");
            //-------------
            country.Clear();
            town.Clear();
            district.Clear();
            street.Clear();
            numberOfBuilding.Clear();
            subNumber.Clear();
            numberOfFlat.Clear();
            Area.Clear();
            numOfRooms1.Checked = false;
            numOfRooms2.Checked = false;
            numOfRooms3.Checked = false;
            numOfRooms4.Checked = false;
            options.ClearSelected();
            material.Text = String.Empty;
            yearPicker1.Value = Convert.ToDateTime(DateTime.Now);
            level.Clear();
            for (int i = 0; i < 4; i++)
            {
                options.SetItemChecked(i, false);
            }
            /*ОЧИСТИТЬ ПОЛЯ ПЛЗ_*/
            rooms = new List<Room>();
            UpdateList();


        }
        public void UpdateList()
        {
            FlatViewer.Clear();

            foreach (var flat in FlatModel.shared.flats)
            {
                string reternstr = "";
                reternstr = flat.adress.country + " ";
                reternstr += flat.adress.town + " ";
                reternstr += flat.adress.street + " ";
                reternstr += flat.adress.subNumberOfBuilding + " ";
                reternstr += flat.adress.subNumberOfBuilding + " ";
                reternstr += flat.adress.numberOfFlat + " ";
                reternstr += flat.size + " ";
                reternstr += flat.numOfRooms + " ";
                reternstr += Show(flat.options) + " ";
                reternstr += flat.typeOfMaterial + " ";
                reternstr += flat.year + " ";
                reternstr += flat.level + " ";
                reternstr += "(";
                foreach (var room in flat.rooms)
                {
                  
                    reternstr += room.area + " ";
                    reternstr += room.numberOfWindows + " ";
                    switch(room.sideOfWindows)
                    {
                        case Side.East:
                            reternstr += "Восточная сторона";
                            break;
                        case Side.North:
                            reternstr += "Северная сторона";
                            break;
                        case Side.West:
                            reternstr += "Западная сторона";
                            break;
                        case Side.South:
                            reternstr += "Южная сторона";
                            break;
                        default: break;
                    }
                    if (flat.rooms.Count > 1)
                    {
                        reternstr += ", ";
                    }
                }

                reternstr += ")";
                reternstr += " Цена: ";
                reternstr += Convert.ToString(FlatModel.shared.Price(flat)) + "$";




                FlatViewer.Items.Add(reternstr);
            }
        }

        private void ConstructorOfFlat_Load(object sender, EventArgs e)
        {

        }
        private string Show(List<Option> ts)
        {
            string a = "";
            foreach (var item in ts)
            {
                switch (item)
                {
                    case Option.Balkon:
                        a+= " Есть балкон";
                        break;
                    case Option.Podval:
                        a += " Есть подвал";
                        break;
                    case Option.SeparatedWC:
                        a += " Раздельный санузел";
                        break;
                    case Option.Studio:
                        a += "Студия";
                        break;
                    default:
                        break;
                }
            }
            return a;
        }

      

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            level.Text = String.Format(Convert.ToString(trackBar1.Value));
        }

        private void save_Click(object sender, EventArgs e)
        {
            FlatModel.shared.Save();
        }

        private void sideOfWindows_KeyPress(object sender, KeyPressEventArgs e)
        {
           if( e.Handled = !Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Пожалуйста, выберите из списка!");
            }
        }

        private void AreaOfRoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled == Char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Пожалуйста, введите число!");
            }
        }

        private void FlatViewer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void material_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !Char.IsSymbol(e.KeyChar))
            {
                MessageBox.Show("Пожалуйста, выберите из списка!");
            }
        }
    }
}
