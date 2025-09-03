# 🔄 Thread-Safe Count Server

Проект демонстрирует реализацию потокобезопасного сервера счетчика на C# с использованием паттерна Reader-Writer Lock.

## 🎯 Цель проекта

Изучение и практическое применение:
- Многопоточного программирования в C#
- Синхронизации доступа к общим ресурсам
- Паттерна Reader-Writer Lock
- Написания unit-тестов для многопоточного кода
- Организации проекта по enterprise-стандартам

## 📦 Структура решения
CountServerSolution/
├── CountServer/ # Class Library - основная логика
│ ├── Server.cs # Потокобезопасная реализация
│ └── CountServer.csproj
├── CountServer.Tests/ # xUnit Test Project
│ ├── ServerTests.cs # Юнит-тесты
│ └── CountServer.Tests.csproj
├── CountServer.ConsoleApp/ # Console Application - демо
│ ├── Program.cs # Пример использования
│ └── CountServer.ConsoleApp.csproj
└── CountServerSolution.sln # Solution файл
