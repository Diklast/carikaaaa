using System;
using System.Collections.Generic;

public class Car
{
    private string brand;
    private bool isRunning;
    private int gear;
    private int speed;

    public Car(string brand)
    {
        this.brand = brand;
        isRunning = false;
        gear = 0;
        speed = 0;
    }

    public void Start(string message)
    {
        if ((gear == 0 || gear == 1) && !isRunning)
        {
            isRunning = true;
            Console.WriteLine($"{message} Машина {brand} завелась.");
        }
        else
        {
            Console.WriteLine($"{message} Нельзя завести машину {brand} в текущей передаче.");
        }
    }

    public void Stop(string message)
    {
        isRunning = false;
        speed = 0;
        Console.WriteLine($"{message} Машина {brand} остановилась.");
    }

    public void Accelerate(string message)
    {
        if (isRunning && gear != 0 && speed < MaxSpeedForGear(gear))
        {
            speed += 10;
            Console.WriteLine($"{message} Скорость машины {brand} увеличена до {speed} км/ч.");
        }
        else
        {
            Console.WriteLine($"{message} Машина {brand} не может увеличить скорость в текущем состоянии.");
        }
    }

    public void Brake(string message)
    {
        if (speed > 0)
        {
            speed -= 10;
            Console.WriteLine($"{message} Скорость машины {brand} уменьшена до {speed} км/ч.");
        }
        else
        {
            Console.WriteLine($"{message} Машина {brand} уже остановлена.");
        }
    }

    public void ChangeGear(string message, int newGear)
    {
        if (isRunning && newGear >= 0 && newGear <= 5 && CheckSpeedForGear(newGear))
        {
            gear = newGear;
            Console.WriteLine($"{message} Передача машины {brand} изменена на {gear}-ю.");
        }
        else
        {
            Console.WriteLine($"{message} Невозможно переключить на {newGear}-ю передачу в машине {brand}.");
        }
    }

    private bool CheckSpeedForGear(int newGear)
    {
        return newGear switch
        {
            0 => true,
            1 => speed >= 0 && speed <= 30,
            2 => speed >= 20 && speed <= 50,
            3 => speed >= 40 && speed <= 70,
            4 => speed >= 60 && speed <= 90,
            5 => speed >= 80 && speed <= 120,
            _ => false
        };
    }

    private int MaxSpeedForGear(int gear)
    {
        return gear switch
        {
            1 => 30,
            2 => 50,
            3 => 70,
            4 => 90,
            5 => 120,
            _ => 0
        };
    }

    public override string ToString()
    {
        return $"{brand} - Скорость: {speed} км/ч, Передача: {gear}, Заведена: {isRunning}";
    }
}

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car("Lamba"),
            new Car("Devyatka"),
            new Car("Mers")
        };

        Console.WriteLine("Выберите машину:");
        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i]}");
        }

        int selectedCarIndex = int.Parse(Console.ReadLine()) - 1;
        Car selectedCar = cars[selectedCarIndex];
        Console.WriteLine($"Вы выбрали машину: {selectedCar}");

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Завестись");
            Console.WriteLine("2. Заглушить");
            Console.WriteLine("3. Ускориться");
            Console.WriteLine("4. Притормозить");
            Console.WriteLine("5. Переключить передачу");
            Console.WriteLine("6. Закончить тест");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    selectedCar.Start("Выбрали:");
                    break;
                case "2":
                    selectedCar.Stop("Выбрали:");
                    break;
                case "3":
                    selectedCar.Accelerate("Выбрали:");
                    break;
                case "4":
                    selectedCar.Brake("Выбрали:");
                    break;
                case "5":
                    Console.WriteLine("Введите номер передачи (0-5):");
                    if (int.TryParse(Console.ReadLine(), out int newGear))
                    {
                        selectedCar.ChangeGear("Вы:", newGear);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод передачи.");
                    }
                    break;
                case "6":
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Некорректная команда.");
                    break;
            }
            Console.WriteLine(selectedCar);
        }
    }
}

