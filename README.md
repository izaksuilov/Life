# Life
Игра «Жизнь» — это игра без игроков, в которой человек создаёт начальное состояние, а потом лишь наблюдает за её развитием. 

## Правила игры

* Место действия игры — размеченная на клетки плоскость, которая может быть безграничной, ограниченной или замкнутой.
* Каждая клетка на этой поверхности имеет восемь соседей, окружающих её, и может находиться в двух состояниях: быть «живой» (заполненной) или «мёртвой» (пустой).
* Распределение живых клеток в начале игры называется первым поколением. Каждое следующее поколение рассчитывается на основе предыдущего по таким правилам:
  * в пустой (мёртвой) клетке, с которой соседствуют три живые клетки, зарождается жизнь;
  * если у живой клетки есть две или три живые соседки, то эта клетка продолжает жить; в противном случае (если живых соседей меньше двух или больше трёх) клетка умирает («от одиночества» или «от перенаселённости»).
* Игра прекращается, если:
  * на поле не останется ни одной «живой» клетки;
  * конфигурация на очередном шаге в точности (без сдвигов и поворотов) повторит себя же на одном из более ранних шагов (складывается периодическая конфигурация)
  * при очередном шаге ни одна из клеток не меняет своего состояния (частный случай предыдущего правила, складывается стабильная конфигурация)

# Значение игры
Игра "Жизнь" и её модификации более сорока лет привлекает внимание исследователей. Она повлияла на многие разделы математики, информатики, физики:
* теорию автоматов;
* теорию игр и математическое программирование;
* комбинаторику и теорию графов;
* фрактальную геометрию и пр.

## Фигуры
**Несмотря на простоту правил, в игре может возникать огромное разнообразие форм.**

Некоторые фигуры остаются неизменными во всех последующих поколениях, состояние других периодически повторяется, в некоторых случаях со смещением всей фигуры. Существует фигура (Diehard) всего из семи живых клеток, потомки которой существуют в течение ста тридцати поколений, а затем исчезают.

К настоящему времени более-менее сложилась следующая классификация фигур:

* Устойчивые фигуры: фигуры, которые остаются неизменными;
* Долгожители: фигуры, которые долго меняются, прежде чем стабилизироваться;
* Периодические фигуры: фигуры, у которых состояние повторяется через некоторое число поколений, большее 1;
* Двигающиеся фигуры: фигуры, у которых состояние повторяется, но с некоторым смещением;
* Ружья: фигуры с повторяющимися состояниями, дополнительно создающие движущиеся фигуры;
* Паровозы: двигающиеся фигуры с повторяющимися состояниями, которые оставляют за собой другие фигуры в качестве следов;
* Пожиратели: устойчивые фигуры, которые могут пережить столкновения с некоторыми двигающимися фигурами, уничтожив их;
* Отражатели: устойчивые или периодические фигуры, способные при столкновении с ними движущихся фигур поменять их направление;
* Размножители: конфигурации, количество живых клеток в которых растёт как квадрат количества шагов;
* Фигуры, которые при столкновении с некоторыми фигурами дублируются.

## Пример

В классических правилах игрок не принимает активного участия в игре: он лишь расставляет или генерирует начальную конфигурацию «живых» клеток, которые затем изменяются согласно правилам. Но в данной реализации имеется возможность редактировать систему в любой момент: 
* Изменить скорость смены поколений;
* Остановить;
* Запустить;
* Вернуться к предыдущему поколению;
* Перейти к следующему поколению;
* Изменить состояние клеток.

![Life_Example](https://user-images.githubusercontent.com/60542253/216816282-d50251f2-b136-4507-85aa-8cf69b3ae1ac.gif)


