using System.Text;

namespace EcrypterApp
{
    //Класс шифрования методом транспонирования
    internal class TransposeCypher : IEncryptable
    {
        //Метод шифрования
        public string Encrypt(string text, int key)
        {
            //Если ключ не находится в диапазоне исходной строки
            //Вернуть строку не меняя её
            if (key <= 0 || key >= text.Length) return text;
            //Создать матрицу символов из исходной строки
            char[,] matrix = GetCharMatrix(text, key);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    sb.Append(matrix[i, j]);
            return sb.ToString();
        }

        //Метод рассчёта высоты матрицы
        int GetMatrixHeight(string text, int key)
        {
            return text.Length / key + ((text.Length % key) == 0 ? 0 : 1);
        }

        //Метод формирования матрицы символов
        char[,] GetCharMatrix(string text, int key)
        {
            //Получить высоту матрицы (количество строк)
            int height = GetMatrixHeight(text, key);
            //Создать массив символов рамером (высота, ключ)
            char[,] matrix = new char[height, key];
            //Переменные номер символа и закончился ли исходный текст
            int charnum = 0; bool end = false;
            //Заполнить матрицу символами исходный строки
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[j, i] = !end ? text[charnum] : ' ';
                    if (charnum < text.Length - 1)
                        charnum++; 
                    else end = true;
                }
            }
            //Вернуть матрицу
            return matrix;
        }

        //Метод дешифрования
        public string Decrypt(string text, int key)
        {
            //Если ключ не находится в диапазоне исходной строки
            //Вернуть строку не меняя её
            if (key <= 0 || key >= text.Length) return text;
            //Вызвать метод шифрования с ключом равным рассчитаной высоте матрицы 
            //Вернуть результат шифрования
            return Encrypt(text, GetMatrixHeight(text, key));
        }
    }
}
