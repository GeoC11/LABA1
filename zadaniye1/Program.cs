using System;

namespace HslColorLab
{
    public class HslColor : IEquatable<HslColor>
    {
        private readonly double _hue;  /*оттенок*/
        private readonly double _saturation; /*насыщенность*/
        private readonly double _lightness; /*светлота*/

        //нормализация
        private static double ClampHue(double value)
        {
            if (value < 0) return 0;
            if (value > 360) return 360;
            return value;
        }

        private static double ClampSaturation(double value)
        {
            if (value < 0) return 0;
            if (value > 100) return 100;
            return value;
        }

        private static double ClampLightness(double value)
        {
            if (value < 0) return 0;
            if (value > 100) return 100;
            return value;
        }

        public HslColor(double hue, double saturation, double lightness)/* Конструктор*/
        {
            _hue = ClampHue(hue); /*нормализуем значения*/
            _saturation = ClampSaturation(saturation);
            _lightness = ClampLightness(lightness);
        }

        //Свойства только для чтения
        public double Hue
        {
            get { return _hue; }
        }

        public double Saturation
        {
            get { return _saturation; }
        }

        public double Lightness
        {
            get { return _lightness; }
        }

        //переопределение метода ToString()
        public override string ToString()
        {
            return $"hsl({_hue}, {_saturation}%, {_lightness}%)";
        }


        //Перегрузки
        public static HslColor operator +(HslColor left, HslColor right)
        {
            return new HslColor(
                left._hue + right._hue,
                left._saturation + right._saturation,
                left._lightness + right._lightness
            );
        }

        public static HslColor operator -(HslColor left, HslColor right)
        {
            return new HslColor(
                left._hue - right._hue,
                left._saturation - right._saturation,
                left._lightness - right._lightness
            );
        }

        public static HslColor operator *(HslColor left, HslColor right)
        {
            return new HslColor(
                left._hue * right._hue,
                left._saturation * right._saturation,
                left._lightness * right._lightness
            );
        }

        public static HslColor operator /(HslColor left, HslColor right)
        {
            if (Math.Abs(right._hue) < 0.0001 ||
                Math.Abs(right._saturation) < 0.0001 ||
                Math.Abs(right._lightness) < 0.0001)
                throw new DivideByZeroException("Division by zero");

            return new HslColor(
                left._hue / right._hue,
                left._saturation / right._saturation,
                left._lightness / right._lightness
            );
        }

        public static bool operator ==(HslColor left, HslColor right)
        {
            //проверяем ссылаются ли переменные на один и тот же объект в памяти
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return Math.Abs(left._hue - right._hue) < 0.0001 &&
                   Math.Abs(left._saturation - right._saturation) < 0.0001 &&
                   Math.Abs(left._lightness - right._lightness) < 0.0001;
        }

        public static bool operator !=(HslColor left, HslColor right)
        {
            return !(left == right);
        }


        //Переопределение метода Equals
        public override bool Equals(object? obj)
        {
            return Equals(obj as HslColor);
        }

        public bool Equals(HslColor? other)
        {
            if (other is null) return false;
            return Math.Abs(_hue - other._hue) < 0.0001 &&
                   Math.Abs(_saturation - other._saturation) < 0.0001 &&
                   Math.Abs(_lightness - other._lightness) < 0.0001;
        }

    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine(" Демонстрация класса HslColor\n");

            // Создание объектов (значения заданы в коде)
            Console.WriteLine("Создание двух цветов\n");

            HslColor color1 = new HslColor(210, 50, 60);
            HslColor color2 = new HslColor(100, 20, 30);

            Console.WriteLine($"\n Цвет 1: {color1}");
            Console.WriteLine($" Цвет 2: {color2}");

            // Все операторы
            Console.WriteLine("\n Демонстрация всех перегруженных операторов \n");

            Console.WriteLine($"Сложение: {color1} + {color2} = {color1 + color2}");
            Console.WriteLine($"Вычитание: {color1} - {color2} = {color1 - color2}");
            Console.WriteLine($"Умножение: {color1} * {color2} = {color1 * color2}");

            try
            {
                Console.WriteLine($"Деление: {color1} / {color2} = {color1 / color2}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Деление: Ошибка - {ex.Message}");
            }

            Console.WriteLine($"\nСравнение: {color1} == {color2} -> {color1 == color2}");
            Console.WriteLine($"Сравнение: {color1} != {color2} -> {color1 != color2}");

            // Неизменяемость
            Console.WriteLine("\nДоказательство неизменяемости\n");

            HslColor original = new HslColor(120, 80, 40);
            Console.WriteLine($"\nИсходный цвет: {original}");

            HslColor added = new HslColor(10, 5, 5);

            HslColor result = original + added;

            Console.WriteLine($"\n Результат сложения: {original} + {added} = {result}");
            Console.WriteLine($"Исходный цвет после операции: {original}");
            Console.WriteLine("\n Исходный объект не изменился!");

            // Граничные случаи
            Console.WriteLine("\n Граничные случаи \n");

            HslColor boundaryColor = new HslColor(-50, 150, 200);

            Console.WriteLine($"\n Введено: hue=-50, saturation=150%, lightness=200%");
            Console.WriteLine($" После нормализации: {boundaryColor}");
            Console.WriteLine(" Значения автоматически приведены к допустимым границам.");

            // Обработка ошибок
            Console.WriteLine("\nОбработка ошибок\n");

            Console.WriteLine("Создадим цвет с нулевыми компонентами (0, 0, 0):");
            HslColor zeroColor = new HslColor(0, 0, 0);
            Console.WriteLine($"Нулевой цвет: {zeroColor}");

            HslColor dividend = new HslColor(100, 50, 25);

            Console.WriteLine($"\n Пробуем выполнить деление: {dividend} / {zeroColor}");

            try
            {
                HslColor divisionResult = dividend / zeroColor;
                Console.WriteLine($"Результат: {divisionResult}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
                Console.WriteLine("Исключение обработано корректно!");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}