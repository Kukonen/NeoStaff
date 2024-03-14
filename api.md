# Api

**use prfix /api/staff/activity/**
- [input](#input)

## input

общее для input

**/employees** [GET]
Входные - нет

Выходные:
- employees - [
    {
        табельный номер - string
        ФИО - string
    }
]

**/activity** [POST]

Входные
    - табельный номер: sting
    - date: string
    - salaryChange: number // изменение ЗП
    - note: string // небольшая заметка HR'а
    - mark: number // субъективная оценка от HR'а
    - activity: {} // активность
Получаемые

    - отсутствует

#### Activities

Активность может иметь один из типов:

- [start](#start) // Вступление в должность
- [end](#end) // Окончание должности
- [certification](#certification) // Аттестация
- [learn](#learn) // Обучение
- [competition](#competition) // Соревнование (например, хакатон)
- [event](#event) // Мероприятие (например, лекция)
- [endTestPeriod](#endTestPeriod) // Окончание испытательного периода
- [changeSalary](#changeSalary) // Изменение ЗП
- [skills](#skills) // Получение каких-то навыков/ повышение квалификации
- [projectStart](#projectStart) // Начало работы над проектом
- [endProject](#endProject) // Окончание работы над проектом
- [careerDialog](#careerDialog) // Карьерный диалог
- [rebuke](#rebuke) // Нарушение/выговор

##### **start**

Предварительно получаем

- positions: [{
    id: string,
    title: string
}] // все позиции компании

Отправка

- position: string // id позиции

##### **end**

Предварительно получаем

- positions: [{
    id: string,
    title: string
}] // возможные должности

Отправка

- position: string // id позиции

##### **certification**
Предварительно получаем c API компании

- certifications: [{
    id: string,
    title: string
}] // все аттестации 

Отправка
- certification: string // id аттестации
- result: string // результат аттестации

##### **learn**
Предварительно получаем

- Ничего

Отправка

- place: string // место
- result: string // результат обучения
- specialization: string // направление обучения
- document: string // ссылка на документ, подтверждающий обучение

##### **competition**
Предварительно получаем

- Ничего

Отправка

- place: string // место
- result?: string // результат соревнование
- theme: string // тема соревнования
- role: "participant" // участник 
        "organizer" // организатор
        "jury" // член жюри

##### **event**
Предварительно получаем

- Ничего

Отправка

- place: string // место
- theme: string // тема события
- role: "participant" // участник
        "speaker" // выступающий

#####  **endTestPeriod**
Предварительно получаем c API компании

- positions: [{
    id: string,
    title: string
}] // позиции человека

Отправка

- position: string // id вакансии, по которой проходился испытательный период
- report: string // отзыв руководителя
- result: "hired" // принят в штат
        "fired" // уволен 
        "extended" // увеличен испытательный срок


#####  **changeSalary**
Предварительно получаем c API компании

- positions: [{
    id: string,
    title: string
}] // позиции человека

Отправка

- position: string // id вакансии, по которой повышена ЗП
- reason: string // причина изменения зарплаты
- value: number // величина изменения в рублях

#####  **skills**
Предварительно получаем
 
Ничего

Отправка
- skill: string // название навыка
- report?: string // отзыв поставщика курсов/образовательных программ

#####  **projectStart**
Предварительно получаем c API компании

- projects: [{
    id: string,
    title: string
}] // проекты
- positions: [{
    id: string,
    title: string
}] // позиции человека

Отправляем:
- role: string // роль в проекте
- position: string // id позиции/специальности в проекте

#####  **endProject**
Предварительно получаем c API компании

- projects: [{
    id: string,
    title: string
}] // незаконченные проекты, в которых участвует сотрудник

Отправляем:
- result: string // итог проекта

#####  **careerDialog**
- positions: [{
    id: string,
    title: string
}] // позиции человека

Отправляем:
- result: "positive"
        "negative"
        "neural" // результат диалога
- report: string // отчет интервьюера о сотруднике

##### **rebuke** (выговор)
Предварительно получаем:
- Ничего

Отправка:
- reason: string