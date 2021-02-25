## Что это
Курсовой проект, который мы делали для обучающих курсов в компании itransition, по итогу которых нас могли бы взять на работу, но не взяли "из-за сложившейся ситуации".
До этого проекта с dotnet никогда не работали. Собственно, основной целью проекта и было научиться использовать dotnet.

## Требования
Требуется разработать сайт для фанфиков ("Мордор — техногенная цивилизация, опороченная победителями").

Неаутентифицированным пользователи могут только читать произведения (доступен поиск, недоступно создание произведений, комментарии, лайки и рейтинги).

Аутентифицированные пользователи имеют доступ ко всему, кроме админки.

Админка позволяет управлять пользоователями (просматривать, блокировать, удалять, назначать админами). Администратор видит каждую страницу пользователя и каждое произведение как автор (например, может редактировать или создать от имени пользователя с его страницы новое произведение).

Требуется поддерживать регистрацию с отправкой мыла с подтверждением (до подтверждения доступа нет), аутентификация через сайт.

На каждой странице доступен полнотекстовый поиск по сайту (результаты поиска — всегда произведения, например, если текст найден в комментарии к произведению, что должно быть возможно, то выводится ссылка на произведение).

У каждого пользователя есть его личная страница, на которой он управляет списком своих произведений (таблица с фильтраций и сортировками, возможность создать/удалить/редактировать произведение/открыть в режиме чтения), поля с информаций о пользователе (in-place editing) и "медальки" (последние — задание со звездочкой).

Каждое произведение состоит из: название, краткое описание, жанр (из фиксированного набора жанров, например, "Фантастика", "Эротика" и проч.), тэги (вводится несколько тэгов, необходимо автодополнение - когда пользователь начинает вводить тэг, выпадает список с вариантами слов, которые уже вводились ранее на сайте). Помимо этого, произведение состоит из "глав" - название главы, блок текста с поддержкой форматирования markdown и одной опциональной картинкой. Глава автомагически формируют оглавление с автонумерацией и набор элементов управления для перехода по главам. На странице произведения главы можно добавлять/удалять/открывать на редактирование/изменять порядок (автомагическая перенумерация).

Все картинки сохраняются в облаке, все картинки загружаются драг-н-дропом.

На главной странице отображаются: последние обновленные произведения, произведения с самыми большими рейтингами, облако тэгов.

При открытие произведения в режиме чтения в конце отображаются комментарии (общие на всю произведение, не отдельно по главам). Комментарии линейные, нельзя комментировать комментариий, новый добавляется только "в хвост". Необходимо реализовать автоматическую подгрузку комментариев — если у меня открыта страница с комментариями и кто-то другой добавляет новый, он у меня автомагически появляется (возможна задержка в 2-5 секунд).

Каждый пользовать может проставить "рейтинг" (от 1 до 5 звездочек) произведения (не более одного от одного пользователя на произведение) — средний рейтинг отображется у произведения. У каждой главы (в конце главы в режиме чтения) пользователь может поставить лайк (не более одного на одну главу от одного пользователя).

Сайт должен поддерживать два языка: русский и английский (пользователь выбирает и выбор сохраняется). Сайт должен поддерживать два оформления (темы): светлое и темное (пользователь выбирает и выбор сохраняется).

Обязательно: Bootstrap (или любой другой CSS-фреймворк), адаптивная вёрстка, поддержка разных разрешений (в том числе телефон), ORM для доступа к данным, движок для полнотекстового поиск (или средствами базы, или отдельный движок — НЕ ПОЛНОЕ СКАНИРОВАНИЕ селектами).

Требования со звездочкой (после реализации всех остальных требований):

* Дополнительно аутентификация через социальные сети (Facebook, Twitter, etc.).

* Экспорт произведений в PDF/Word (при отображении произведения добавить кнопку, которая позволяет скачать произведение в PDF.

* Поддержка "медалек" - по достижению какого-то результата на странице пользователя отображается "медалька" (маленькие версии отображаются везде на сайте после имени пользователя, а само имя всегда является ссылкой на страницу пользователя). Например: "написал 3 щедевра", "собрал 10 лайков", "получил за произведение рейтинг 4+ с количеством отзывов 10+" и проч. (не менее разных 4 медалей).

* Реализация "тезауруса" - при длинном нажатии на слово появляется список синонимов (не изобретать и не пилить с нуля, конечно).

## Выполненые требования
- полная и мобильная версии
- аутентификация пользователей
- админка
- подтверждение почты (с нашего имейла:)
- личная страница (только с произведениями этого пользователя)
- создание/редактирование/чтение произведений, состоящих из глав, поддержка маркдауна(типа круто)
- комментарии на сигналР
- темная тема
- динамически подгружаемая фид лента (как в вк)

## Что получилось

<a href="https://www.youtube.com/watch?v=zJPIfkTtTI0&feature=youtu.be" target="_blank"><img src="https://img.youtube.com/vi/zJPIfkTtTI0/hqdefault.jpg" 
alt="IMAGE ALT TEXT HERE" width="240" height="180" border="10" /></a>
