# Api

**use prfix /api/staff/activity/**
- [input](#input)

## input

общее для input

**/empolyers** [GET]
Входные - нет

Выходные:
- employers - [
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

##### Activity

Активность может иметь один из типов:

- start // название должности
- end // окончание должности
- certification // аттестация
- learn // обучение
- competition // соревнование (например, хакатон)
- event // мероприятие (нппример, лекция)
- endTestPeriod // окончание испытательного периода
- changeSalary // изменение ЗП
- skills // получение каких-то навыков/ повышение квалификации
- projectStart // начало работы над проектом
- endProject // окончание работы над проектом
- careerDialog // карьерный диалог

**start**

Предварительно получаем

- positions: [{
    id: string,
    title: string
}] // все позиции компании

Отправка

- position: string // id позиции

**end**

Предварительно получаем

- positions: [{
    id: string,
    title: string
}] // позиции человека

Отправка

- position: string // id позиции

**certification**

Предварительно получаем

- certifications: [{
    id: string,
    title: string
}] // все сертификации 

Отправка
- certification: string // id сертификации
- result: string // результат аттестации

**learn**

Предварительно получаем

- Ничего

Отправка

- place: string // место
- result: string // результат обучения
- specialization: string // направление обучения
- document: string // ссылка на документ, подтверждающий обучение

**competition**

Предварительно получаем

- Ничего

Отправка

- place: string // место
- result?: string // результат соревнование
- theme: string // тема соревнования
- role: "participant" // участник 
        "organizer" // организатор
        "jury" // член жюриы