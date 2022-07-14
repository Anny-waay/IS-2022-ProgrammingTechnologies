# Отчет

<h2>Запуск изначальной программы</h2> 

На 100 файлах

Данные по времени работы:
<img width="1399" alt="1 version 100 files" src="https://user-images.githubusercontent.com/79156521/168257697-0236ed98-c850-4b7c-b8c0-050444b3a529.png">
<img width="1370" alt="1 version 100 files graph" src="https://user-images.githubusercontent.com/79156521/168257786-4d4d2f19-c66a-449e-bd32-6c397cc04093.png">

Данные по аллокации памяти:
<img width="1398" alt="1 version 100 files mem" src="https://user-images.githubusercontent.com/79156521/168257997-7d7c00c6-3959-4ffe-acd1-c85a7c6ac428.png">
<img width="1368" alt="1 version 100 files mem graph" src="https://user-images.githubusercontent.com/79156521/168258021-5a18ad75-4669-4a3c-b0de-e061d9e42119.png">

На 1000 файлах

Данные по времени работы:
<img width="1401" alt="1 version 1000 files" src="https://user-images.githubusercontent.com/79156521/168258950-0fbd905a-3843-4e08-bc55-f8ef1d27aa31.png">
<img width="1383" alt="1 version 1000 files graph" src="https://user-images.githubusercontent.com/79156521/168258986-c7ebd3eb-0163-4ea6-9e95-f815f472d0eb.png">

Данные по аллокации памяти:
<img width="1398" alt="1 version 1000 files mem" src="https://user-images.githubusercontent.com/79156521/168259035-5851533e-ffe2-4ab8-8d07-2b031e02c405.png">
<img width="1384" alt="1 version 1000 files mem graph" src="https://user-images.githubusercontent.com/79156521/168259054-e13ece11-d01a-4ef3-aeac-712cd6ca7c3c.png">

По запуску на 1000 файлах видно, что много времени тратится на функцию FindSize, которая ищет размер ноды на текущий момент. Тогда возникает идея хранить в сущности ноды размер и менять его при добавлении и удалении файлов, однако тогда приходится заменить хранение сущности в структуре на класс.

<h2>Запуск программы без функции FindSize</h2> 

На 100 файлах

Данные по времени работы:
<img width="1414" alt="change FindSize 100 files " src="https://user-images.githubusercontent.com/79156521/168260133-4288d5aa-3b32-40c1-b3b5-9786d3bc2113.png">
<img width="1386" alt="change FindSize 100 files graph" src="https://user-images.githubusercontent.com/79156521/168260155-6e9f5dd4-b133-4ed3-a244-d83a1d4688a9.png">

Данные по аллокации памяти:
<img width="1392" alt="change FindSize 100 files mem" src="https://user-images.githubusercontent.com/79156521/168260193-d1246650-eca2-42b3-99cc-d06395dfc686.png">
<img width="1362" alt="change FindSize 100 files mem graph" src="https://user-images.githubusercontent.com/79156521/168260213-93f47d03-51f6-4e8a-a661-ecc245921351.png">

На 1000 файлах

Данные по времени работы:
<img width="1381" alt="change FindSize 1000 files" src="https://user-images.githubusercontent.com/79156521/168260259-9e5178d5-29b9-4cf3-ae13-30b5bb7a9032.png">
<img width="1363" alt="change FindSize 1000 files graph" src="https://user-images.githubusercontent.com/79156521/168260341-5b671dde-a687-4348-b668-25cfea1bc3bd.png">

Данные по аллокации памяти:
<img width="1392" alt="change FindSize 1000 files mem" src="https://user-images.githubusercontent.com/79156521/168260369-f38503bb-d17b-40c8-a349-c4949bc4f708.png">
<img width="1364" alt="change FindSize 1000 files mem graph" src="https://user-images.githubusercontent.com/79156521/168260387-d064e655-c788-4226-b817-f19b4c8d2d7d.png">

На 100 файлах мы видим уменьшение времени работы и такой же расход по памяти, однако на 1000 файлов время работы значительно увеличилось: функция ConnectClient стала работать в разы дольше, из-за того что для подключения она обращается к порту в сущности ноды, а так как структура была заменена на класс, это обращение стало занимать больше времени.

Из-за увелечения времени работы на большом количестве файлов от этой идеи было принято решение отказаться и попытаться оптимизировать работу засчет StringBuilder вместо сложения строк.

<h2>Запуск программы с StringBuilder</h2> 

На 100 файлах

Данные по времени работы:
<img width="1398" alt="StringBuilder 100 files" src="https://user-images.githubusercontent.com/79156521/168261890-9976007e-b687-4382-9f4b-a3fcbac46f51.png">
<img width="1370" alt="StringBuilder 100 files graph" src="https://user-images.githubusercontent.com/79156521/168261917-83f82c7d-d715-49b8-9955-b0d16b8c674f.png">

Данные по аллокации памяти:
<img width="1397" alt="StringBuilder 100 files mem" src="https://user-images.githubusercontent.com/79156521/168261932-074b234b-7bac-4a31-9c6d-c1f2769cb16f.png">
<img width="1371" alt="StringBuilder 100 files mem graph" src="https://user-images.githubusercontent.com/79156521/168261948-f98479ab-f0fe-4891-a5ae-f8f31854dca4.png">

На 1000 файлах

Данные по времени работы:
<img width="1400" alt="StringBuilder 1000 files" src="https://user-images.githubusercontent.com/79156521/168261781-fd2ce2a8-9a01-45dc-8146-1e0ae7ddcdb1.png">
<img width="1377" alt="StringBuilder 1000 files graph" src="https://user-images.githubusercontent.com/79156521/168261826-1cb5a92b-4a9e-428a-9206-ed2878fca1c9.png">

Данные по аллокации памяти:
<img width="1399" alt="StringBuilder 1000 files mem" src="https://user-images.githubusercontent.com/79156521/168261850-160bdbee-fe95-403c-b968-38fa049411b2.png">
<img width="1375" alt="StringBuilder 1000 files mem graph" src="https://user-images.githubusercontent.com/79156521/168261867-76991bcc-1541-4d89-b172-2f718940020e.png">

На 100 файлах видим уменьшение времени работы и такой же расход памяти, на 1000 - время работы уменьшилось где-то на 20%, но выделение памяти увеличилось на 0.8 МБ.

Далее было принято решение изначально задавать размер для списков, если он известен.

<h2>Запуск программы с задаваемым размером списка</h2> 

На 100 файлах

Данные по времени работы:
<img width="1399" alt="ListWithSize 100 files" src="https://user-images.githubusercontent.com/79156521/168263195-89875362-538a-478b-98c4-270a187e73a3.png">
<img width="1373" alt="ListWithSize 100 files graph" src="https://user-images.githubusercontent.com/79156521/168263276-66ab54f6-648c-4183-a74f-5303014f194b.png">

Данные по аллокации памяти:
<img width="1399" alt="ListWithSize 100 files mem" src="https://user-images.githubusercontent.com/79156521/168263302-32e690ff-027e-4be6-9681-6dc882f6cf18.png">
<img width="1371" alt="ListWithSize 100 files mem graph" src="https://user-images.githubusercontent.com/79156521/168263314-e9336129-2f83-4580-884c-5c8d8242a906.png">

На 1000 файлах

Данные по времени работы:
<img width="1401" alt="ListWithSize 1000 files" src="https://user-images.githubusercontent.com/79156521/168263345-ac9ef081-8656-4a4b-a131-4495c9417656.png">
<img width="1373" alt="ListWithSize 1000 files graph" src="https://user-images.githubusercontent.com/79156521/168263373-de077e51-cc26-400a-bbe3-3ffc1f8c054a.png">

Данные по аллокации памяти:
<img width="1400" alt="ListWithSize 1000 files mem" src="https://user-images.githubusercontent.com/79156521/168263392-32d8a0ce-5da5-4401-ab12-7af7344b0009.png">
<img width="1374" alt="ListWithSize 1000 files mem graph" src="https://user-images.githubusercontent.com/79156521/168263405-a8307ded-9482-43a8-a4be-586a98d0948d.png">

На запуск на 100 файлах это изменение практически не повлияло, однако на 1000 уменьшило время работы примерно на 30% и никак не повлияло на память.
