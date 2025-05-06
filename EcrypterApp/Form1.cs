using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EcrypterApp
{
    //Класс формы. Это часть класса, в которой хранится 
    //только рукописный код. Код, который генерируется с помощью
    //конструктора формы, не будет приведён
    public partial class Form1 : Form
    {
        //Массив шифров
        Dictionary<string, IEncryptable> cyphers;

        //Конструктор класса, вызывается при запуске программы
        public Form1()
        {
            InitializeComponent();
            //Заполнение массива шифров
            cyphers = new Dictionary<string, IEncryptable> {
                { "Цезарь", new CesaroCypher() }
            };
            //Заполнение выпадающего списка
            cbFunction.DataSource = cyphers.Keys.ToList();
            //Выбор первого элемента списка
            cbFunction.SelectedIndex = 0;
        }

        //Метод, вызывающийся при нажатии кнопки "-->"
        private void bEncrypt_Click(object sender, EventArgs e)
        {
            //Вызвать метод шифрования класса из массива шифров
            //под выбранном в выпадающем списке номером
            tbCypher.Text = cyphers[cbFunction.Text]
                .Encrypt(tbSource.Text, (int)tbKey.Value);
        }

        //Метод, вызывающийся при нажатии кнопки "<--"
        private void bDecrypt_Click(object sender, EventArgs e)
        {
            //Вызвать метод дешифрования класса из массива шифров
            //под выбранном в выпадающем списке номером
            tbSource.Text = cyphers[cbFunction.Text]
                .Decrypt(tbCypher.Text, (int)tbKey.Value);
        }
    }
}
