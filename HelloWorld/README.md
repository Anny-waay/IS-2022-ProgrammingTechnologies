<H1>Task1</H1>
Процесс интеропа между языками – это возможность из одного языка вызвать код, написанный на другом.

Логика работы: 
Чтобы подключить интероп нужно воспользоваться динамически подключаемой библиотекой. Пишем код на C/C++ и вместо .exe собираем .dylib
Для имплементации методов используем специальные обозначения. Например, в Java мы нужент файл jni.h, из которого мы берём типы

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758057-4c1f2f5f-c2ce-488d-b1de-68da17bfa51c.png">

Использование на Java:

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758120-a6d0f214-0a17-40c3-b76a-4f8477483d45.png">

Загружаем библиотеку по пути к файлу

Подключение библиотеки на C#:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758193-94a45dbb-dc4c-4300-9a75-71c2a200a14b.png">
Используем DllImport для подключения библиотеки, указывая путь к ней.

<B>Плюсы:</B>

Простой способ создать «переносимую» программу(возможность не переписывать код с одного языка на другой)

<B>Минусы:</B>

Сложнее писать код и тестировать

Замедляет выполнение программы

Больше вероятность ошибок

<H1>Task2</H1>

Pipe operator и Discriminated Union на Scala:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758527-a3cfe424-c246-4624-8a94-b8a87f8bb336.png">

Один из классов после декомпиляцию в Java
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758609-25a37519-a2aa-4f4b-a97e-4ba1e1fad349.png">
Также для него определены методы hashCode() toString() equals() и конструктор

Преобразование оператора pipe:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758667-7e3efe2b-5ada-447f-ab61-e24b93e81f3b.png">

Преобразование функции для подсчета площади:
  
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758716-fdf8c73e-7ac1-46e6-a674-b87d591a9f74.png">

Pipe operator и Discriminated Union на F#
 
 <img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758758-aae6b1f9-5155-4b01-b238-fb4c474611ad.png">
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758774-646f43b1-9c61-4182-86e7-1599378a2bf7.png">
 
Один из классов после декомпиляцию в C#

 <img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758831-0ff76538-3911-4ce2-96c3-9dd03a28d5b3.png">

Преобразование оператора pipe:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758862-39de84a4-d82c-45ec-9cc8-0e0fc6f8f16e.png">

Преобразование функции подсчета площади Discriminated Union:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758875-e302063d-1341-4a10-9e88-1423aa1346ba.png">

Преобразование Computation expressions
 
 <img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758893-fb6514d5-45ab-48a0-8f23-0d206a2d4005.png">
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156758909-0e06a0e4-bcf5-4575-9221-036433ee50a0.png">

Вывод: т.к. мы используем в коде уникальные возможности языков Scala и F#, то не имея их аналогов на Java и C#, код становится достаточно громостким(особенно классы).

<H1>Task3</H1>
Пишем алгоритмы DFS и BFS на C# и Java


В C# собираем код в пакет nuget с помощью Rider(Advanced Build Action -> Pack Select Project) и публикуем на сайте nuget, далее можем найти nuget пакет в Rider и подключить его

В Java для создания пакета используем Maven(Maven -> раскрываем наш пакет -> Lyfecycle -> package), получаем jar файл, который можем использовать в проекте на Scala

<H1>Task4</H1>

C#

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156759039-3c514606-51cf-46c2-b642-76201aabed44.png">

При запуске класса, BenchmardDotNet выполняет сначала подготовку, определяя количество итераций и оценивая накладную работу, затем приступает к разогреву и измерению. 

Результаты:
 
<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156759082-dfb47bb1-2012-44e6-854c-83522f17c541.png">

Java 

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156759106-6fb170f1-c607-454b-befa-1d984ebe93eb.png">

Аналогично C# проводим измерения с помощью Java Microbenchmark Harness 

Результаты:

<img width="1000" alt="image" src="https://user-images.githubusercontent.com/79156521/156759131-bfbeecdb-f0df-40d6-abd9-d0b895e1b616.png">

По результатам работы бенчмарков можно получить примерное представление о эффективности сортировок, а также использование ими памяти.

<H1>Task5</H1>

В цикле 100 раз выполняется создание точек, добавление в них файлов, удаление файла из джобы и их удаление точек.

Измерения dotTrace с файловой системой
 
<img width="1000" alt="Снимок экрана 2022-03-04 в 16 59 30" src="https://user-images.githubusercontent.com/79156521/156778860-c9bdb97e-1d3f-43a2-bf70-7ddf8471ca0a.png">

Измерения dotTrace без файловой системы

<img width="1000" alt="Снимок экрана 2022-03-04 в 16 37 27" src="https://user-images.githubusercontent.com/79156521/156778886-9d3b9182-c316-4814-91a2-217e12aabf42.png">

Вывод: основные затраты при работе с файловой системой тратится на архивацию файлов, а без нее на чтение и запись информации о точках при их создании и удалении.
