Wymagania:
.NET Core SDK: Zainstaluj najnowszą wersję .NET Core SDK ze strony .NET Microsoft.
Serwer bazy danych: sql server

Aby zainstalować aplikację pobierz repozytorium z GitHub i otwórz plik projektu
Użyj komendy dotnet restore, aby zainstalować zależności projektu
Użyj dotnet build, aby zbudować aplikację, a następnie dotnet run, aby ją uruchomić.
wykonaj migracje bazy danych przy użyciu dotnet ef database update, aby ustawić schemat bazy danych.

Użytkownik ma możliwość rejestracji, a także logowania do aplikacji korzystając z odpowiednich przycisków
Korzystając z przycisku sklep można przejść do sklepu, w którym można wybrać dla siebie zegarek i dodać go do koszyka
Aplikacja przeniesie wtedy użytkownika automatycznie do widoku koszyka, gdzie można usunąć wybrany przedmiot, wrócić do poprzedniego okna, bądź przejść do finalizacji zamówienia
Po skorzystaniu z przycisku finalizacja użytkownik przechodzi do uzupełniania danych do faktury oraz dostawy

Użytkownik z uprawnieniami administratora ma dodatkowo możliwość dodawać zegarki na stan sklepu, a także edytować ich właściwości
