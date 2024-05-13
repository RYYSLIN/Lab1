
Возможности:

Создание новых файлов
Открытие существующих файлов
Сохранение файлов
Сохранение файлов как
Отмена и повтор действий
Вырезание, копирование и вставка текста
Удаление текста
Выделение всего текста
Поиск и замена текста
Справка
Требования:

Операционная система Windows
Установка:

Запустите установочный файл.
Использование:

Запустите приложение Компилятор.
Создайте новый файл или откройте существующий файл.
Введите или отредактируйте текст в окне редактора.
Сохраните файл, когда закончите.
Советы:

Используйте комбинации клавиш для быстрого доступа к функциям, например Ctrl+S для сохранения файла.
Используйте функцию поиска и замены для поиска и замены текста в файле.
Используйте справку для получения дополнительной информации о возможностях приложения.
Обратная связь:

Если у вас есть какие-либо вопросы или предложения, пожалуйста, свяжитесь с нами по адресу ryyslinchik@mail.ru






2---------------------------------------
![image](https://github.com/RYYSLIN/Lab1/assets/160394383/6f169ff5-ae3d-42df-8531-2e2a60ecf9a1)

1)Постановка задачи
Разработать грамматику.
Изучить назначение лексического анализатора. Спроектировать алгоритм и выполнить программную реализацию сканера.
Спроектировать диаграмму состояний сканера (примеры диаграмм представлены в прикрепленных файлах).
Разработать лексический анализатор, позволяющий выделить в тексте лексемы, иные символы считать недопустимыми (выводить ошибку).
Встроить сканер в ранее разработанный интерфейс текстового редактора. Учесть, что текст для разбора может состоять из множества строк.
2)Пример строк



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/05a75c7a-5b33-4ecd-90b7-d8a0fb0ebf12)


![image](https://github.com/RYYSLIN/Lab1/assets/160394383/461fbc2f-4ecc-4360-938a-c8e27a5c3afa)

3) Пример из моей программы

 
 ![image](https://github.com/RYYSLIN/Lab1/assets/160394383/f1bf076b-b76b-43ba-80d4-afa790e2b418)

3----------------------------------------------------------------------------------------------------------


Лабораторная работа 3

1) Грамматика
   G[ <I> ]: 
Vt = { ‘COMPEX’,‘a’…’z’, ‘A’…’Z’, ‘0’…’9’, ‘:’ ,  ‘,’ , ‘+’, ‘-‘, ‘=’ , ‘(’ , ‘)’, ‘_’} 
Vn = { <I>, iD , ASSIGN , SIGN1,SIGN2, DECMAL1,DECMAL2,DECMAL1REM,DECMAL2REM, OPENBRACKET,CLOSEBRACKET , NUMBER1,NUMBER1REM,NUMBER2,NUMBER2REM} 
P = { 
1.<I> → ‘COMPLEX’ ASSIGN 
2.ASSIGN → ‘::’ID 
3.ID -> letter IDREM
4.IDREM -> ( letter | | digit | _ )IDREM | '=' OPENBRACKET 
5.OPENBRACKET → ‘(’  SIGN1,
6.SIGN1 -> [+ | -] NUMBER1
7.NUMBER1 -> digit NUMBER1REM 
8.NUMBER1REM -> digit NUMBER1REM | '.' DECMAL1 | ',' SIGN2
9.DECMAL1 → digit  DECMAL1REM
10.DECMAL1REM -> digit DECMAL1REM | ',' SIGN2
11.SIGN2 -> [+ | -] NUMBER2
12.NUMBER2 -> digit NUMBER2REM 
13.NUMBER2REM -> digit NUMBER2REM | '.' DECMAL2 | ')'
14.DECMAL1 → digit  DECMAL1REM
15.DECMAL1REM -> digit DECMAL1REM | ')'


2)Примеры верных строк:
COMPLEX::z=(2.0,3.0)
COMPLEX::z=(+2.0,-3.0)
COMPLEX:: XEZ121 =(+42,-4.0)

2)Конечный автомат




![image](https://github.com/RYYSLIN/Lab1/assets/160394383/256a4a2b-21f5-47fc-b749-3cd4a937b004)


3) Окно вывода: верно и неверно



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/59e1e6d9-2ff1-47ce-a965-70b99fc820ba)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/809894ed-162c-483c-a9a3-2576a5d274bb)




![image](https://github.com/RYYSLIN/Lab1/assets/160394383/7c627b31-6448-46ae-a49f-116eaaf4cc5a)




Лабораторная работа 4



Исправление ошибок


![image](https://github.com/RYYSLIN/Lab1/assets/160394383/804fac7b-5ce3-49db-af6b-4025cd34fca4)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/22989293-0204-433f-a161-8d689183aac7)




Лабораторная работа 5




Включение семантики в анализатор. Создание внутренней формы представления программы.


Вариант 1



1 вариант. В качестве внутренней формы представления программы выберем польскую инверсную запись (ПОЛИЗ). Эта форма представления наглядна и достаточно проста для последующей интерпретации, которая может быть выполнена с использованием стека.

Задание:

1) Дополнить парсер грамматикой G[<АВ>]. Реализовать данную КС-граммматику методом рекурсивного спуска:

1. E → TA 

2. A → ε | + TA | - TA 

3. T → ОВ 

4. В → ε | *ОВ | /ОВ 

5. О → num | (E) 

2) Реализовать алгоритм записи арифметических выражений в ПОЛИЗ и алгоритм вычисления выражений в ПОЛИЗ.


![image](https://github.com/RYYSLIN/Lab1/assets/160394383/7c38989b-9852-40c0-80ed-1dfd0533ae42)




![image](https://github.com/RYYSLIN/Lab1/assets/160394383/f189562f-c46d-43ac-b32a-2fc76c6ef320)



Лабораторная работа 6


Реализация алгоритма поиска подстрок с помощью регулярных выражений.



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/ca842a70-dfdb-4014-9e92-100f87880956)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/82ce7f2c-c9e0-4f51-a6ef-2edd678423f6)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/9de5bec0-3334-487a-aa2b-4d79b79fb954)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/43ef28e5-f347-4caf-92bf-0fa7f27035fa)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/ed5db92f-b0e1-4dab-9e84-c18a1b20c152)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/9d0d7e12-9d8f-49f6-9940-6a9ee77fc0e0)



![image](https://github.com/RYYSLIN/Lab1/assets/160394383/fc1018bc-f7c4-418a-8caa-c6af66637361)





Лабораторная  работа 7




![image](https://github.com/RYYSLIN/Lab1/assets/160394383/bdb16758-d2fa-4841-aefd-35f9b55b1e5b)




![image](https://github.com/RYYSLIN/Lab1/assets/160394383/fa065056-c5cb-4397-8d44-32e682e9c62b)





