using System.Linq;
using System.Text;

namespace EcrypterApp
{
    //Класс шифра Цезаря, реализует шифруемый интерфейс
    internal class CesaroCypher : IEncryptable
    {
        //Поля со всеми буквами алфавитов
        string alphaRu = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ";
        string alphaEn = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        //Метод шифрования
        public string Encrypt(string text, int key)
        {
            //Переменная для добавления символов
            StringBuilder sb = new StringBuilder();
            //Начало цикла, для каждого символа в строке исходного текста
            foreach (char c in text)
            {
                //Если не является буквой,
                //добавить в конечную строку
                if (!char.IsLetter(c)) { 
                    sb.Append(c); continue;
                }
                //В каком регистре находится буква
                bool isup = char.IsUpper(c);
                //Конечный символ
                char result;
                //Символ в верхнем регистре
                char capsc = c;
                if (!isup) capsc = char.ToUpper(c);
                //Содержится ли символ в русском алфавите
                if (alphaRu.Contains(capsc))
                    //Выполнить сдвиг если содержится
                    result = Shift(alphaRu, capsc, key);
                //Содержится ли символ в английском алфавите
                else if (alphaEn.Contains(capsc))
                    //Выполнить сдвиг если содержится
                    result = Shift(alphaEn, capsc, key);
                //Если нигде не содержится, добавить в исходном виде
                else {
                    sb.Append(c); continue;
                }
                //Изменить регистр, если требуется, и добавить в конечную строку
                if (!isup) sb.Append(char.ToLower(result));
                else sb.Append(result);
            }
            //Вернуть конечную строку
            return sb.ToString();
        }

        //Метод сдвига, в него передаётся алфавит, символ и размер сдвига
        char Shift(string alphabet, char c, int shift)
        {
            //Номер символа в алфавите
            int num = alphabet.IndexOf(c);
            //Прибавление к номеру символов размера сдвига
            num += shift;
            //Если номер не попадает в диапазон алфавита
            if (num >= alphabet.Length || num < 0) {
                //То номер равен остатку от деления на размер алфавита
                num %= alphabet.Length;
                //Если номер осталя отрицательным, то прибавить размер алфавита
                if (num < 0) num += alphabet.Length;
            }
            //Вернуть символ из алфавита, соответствующий номеру
            return alphabet[num];
        }

        //Метод дешифрования
        public string Decrypt(string text, int key) {
            //Вызвать метод шифрования с отрицательным ключом
            //Вернуть результат шифрования
            return Encrypt(text, -key);
        }

    }
}
