using System;

namespace FacadePattern
{
    //  подсистема 1: склад 
    public class WarehouseService /*класс отвечает за работу со складом*/
    {
        public bool CheckStock(string productId, int quantity) /*ф-ция проверки товара на складе*/
        {
            Console.WriteLine($"[Склад] Проверка наличия товара {productId} в количестве {quantity}");
            return true;
        }

        public void ReserveProduct(string productId, int quantity) /*ф-ция резервирования товара*/
        {
            Console.WriteLine($"[Склад] Товар {productId} зарезервирован в количестве {quantity}");
        }
    }

    // подсистема 2: платежи
    public class PaymentService
    {
        public bool ProcessPayment(string cardNumber, decimal amount) /*обработка платежа*/
        {
            Console.WriteLine($"[Платежи] Обработка платежа {amount:F2} руб. с карты {cardNumber}");
            return true;
        }
    }

    // подсистема 3: доставка
    public class DeliveryService
    {
        public void CreateShippingLabel(string address, string orderId)
        {
            Console.WriteLine($"[Доставка] Создана этикетка для заказа {orderId} по адресу: {address}");
        }
    }

    // подсистема 4: уведомления
    public class NotificationService
    {
        public void SendEmail(string email, string message)
        {
            Console.WriteLine($"[Уведомления] Email на {email}: {message}");
        }
    }

    // ФАСАД
    public class OrderFacade
    {
        //Приватные поля для хранения ссылок на подсистемы
        private readonly WarehouseService _warehouse; /*сервис склада*/
        private readonly PaymentService _payment; /*платёжный сервис*/
        private readonly DeliveryService _delivery; /*сервис доставки*/
        private readonly NotificationService _notification; /*сервис уведомлений*/

        public OrderFacade() /*конструктор*/
        {
            //все сервисы подсистемы
            _warehouse = new WarehouseService();
            _payment = new PaymentService();
            _delivery = new DeliveryService();
            _notification = new NotificationService();
        }

        //оформление заказа
        public void PlaceOrder(string productId, int quantity, string cardNumber, decimal amount, string address, string email)
        {
            Console.WriteLine("\nОформление заказа: \n");

            // Шаг 1: Проверка наличия товара на складе
            if (!_warehouse.CheckStock(productId, quantity))
            {
                Console.WriteLine("\n Ошибка: товара нет в наличии");
                return;
            }

            // Шаг 2: Резервирование товара
            _warehouse.ReserveProduct(productId, quantity);

            // Шаг 3: Оформление платежа
            if (!_payment.ProcessPayment(cardNumber, amount))
            {
                Console.WriteLine("\n Ошибка: оплата не прошла");
                return;
            }

            // Шаг 4: Создание и печать этикетки доставки
            string orderId = Guid.NewGuid().ToString(); /*создаем уникальный идентификатор*/
            _delivery.CreateShippingLabel(address, orderId);

            // Шаг 5: Отправка уведомления покупателю
            _notification.SendEmail(email, $"Ваш заказ {orderId} успешно оформлен и оплачен!");

            Console.WriteLine($"\n Заказ {orderId} успешно оформлен!");
        }
    }

    // клиентский код
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Демонстрация шаблона ФАСАД\n");

            // Клиент создаёт фасад
            OrderFacade facade = new OrderFacade();

            // Клиент вызывает один метод фасада, а не 5+ сервисов по отдельности
            facade.PlaceOrder(
                productId: "iPhone 15",
                quantity: 1,
                cardNumber: "1234-5678-9012-3456",
                amount: 79999.99m,
                address: "г. Москва, ул. Мосфильмовская 15, кв 5",
                email: "customer@example.com"
            );

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}