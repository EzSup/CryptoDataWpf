# README

## Українська (English version below)

### Опис проекту

Цей проект є десктопним додатком для перегляду даних про криптовалюти. Він використовує технології C#/.NET, WPF, патерни MVVM та DI. Для отримання даних про криптовалюти використовуються API "CoinCap" та "KuCoin".

### Функціонал (усі задачі ТЗ виконані)

1. **Меню (зліва)**:
   - Має 4 опції:
     - Перша опція веде на головну сторінку (Сторінка 1 в списку).
     - Друга опція веде на сторінку конвертації валют (Сторінка 3 в списку).
     - Третя і четверта опції дозволяють вибрати кольорову тему та локалізацію додатку.
   - Меню також можна розгорнути, натиснувши на кнопку "BurgerButton" вгорі.

2. **Панель (зверху)**:
   - Відображає заголовок поточної сторінки.
   - Містить поле для введення назви/коду валюти і кнопку пошуку, яка переходить на сторінку 2.

3. **Сторінка 1 (Головна)**:
   - Має список з 20 валют, отриманих з CoinCapAPI, з короткими даними про кожну валюту і посиланням на ринок.
![image](https://github.com/user-attachments/assets/58f8d487-5a22-4548-8a15-c78d94458e73)

4. **Сторінка 2 (Дані валюти)**:
   - Містить максимум даних про валюту: код, назва, ціна, пропозиції, зміна ціни, та посилання на ринок.
   - Має графік зміни курсу за останню годину.
![image](https://github.com/user-attachments/assets/28ee5970-31da-49d5-bfbe-b46d03c60928)

5. **Сторінка 3 (Конвертація валют)**:
   - Містить два блоки для вибору валют (з полями для вводу і пошуку) та вказання кількості валюти для конвертації.
   - Містить блоки з кнопками для зміни валют (перемикання місцями) і конвертації.
   - Кнопки розблоковуються при виборі валют і вказанні кількості більше ніж 0.
![image](https://github.com/user-attachments/assets/332bad06-813c-4ef3-b25a-cbda4c98637d)

**ВАЖЛИВО!** Оскільки використовуються різні API для отримання даних про валюти, іноді графіки цін для деяких валют не завантажуються. У таких випадках ви можете побачити вікно з повідомленням "Currency OHCL data searching exception". Це не є помилкою виконання програми.

![image](https://github.com/user-attachments/assets/9688e261-c984-44f4-a8fc-11337e714506)

---

## English

### Project Description

This project is a desktop application for viewing cryptocurrency data. It utilizes technologies such as C#/.NET, WPF, MVVM patterns, and DI. It retrieves cryptocurrency data using the "CoinCap" and "KuCoin" APIs.

### Features

1. **Menu (Left)**:
   - Contains 4 options:
     - The first option navigates to the home page (Page 1 in the list).
     - The second option navigates to the currency conversion page (Page 3 in the list).
     - The third and fourth options allow you to select the color theme and localization of the application.
   - The menu can also be expanded by clicking the "BurgerButton" at the top.

2. **Panel (Top)**:
   - Displays the title of the current page.
   - Contains a field for entering the currency name/code and a search button that navigates to page 2.

3. **Page 1 (Home)**:
   - Displays a list of 20 currencies retrieved from CoinCapAPI with brief data about each currency and a market link.
![image](https://github.com/user-attachments/assets/58f8d487-5a22-4548-8a15-c78d94458e73)

4. **Page 2 (Currency Data)**:
   - Contains detailed data about the currency: code, name, price, supply, price change, and market link.
   - Includes a chart showing the price change over the last hour.
![image](https://github.com/user-attachments/assets/28ee5970-31da-49d5-bfbe-b46d03c60928)

5. **Page 3 (Currency Conversion)**:
   - Includes two blocks for selecting currencies (with input fields and search options) and specifying the amount of currency for conversion.
   - Includes blocks with buttons for swapping currencies (toggle) and performing the conversion.
   - Buttons become enabled when currencies are selected and the amount is greater than 0.
![image](https://github.com/user-attachments/assets/332bad06-813c-4ef3-b25a-cbda4c98637d)

**IMPORTANT!** Since different APIs are used to retrieve currency data, sometimes price charts for certain currencies may not load. In such cases, you might see a window with the message "Currency OHCL data searching exception". This is not an error in the program execution.


![image](https://github.com/user-attachments/assets/9688e261-c984-44f4-a8fc-11337e714506)
