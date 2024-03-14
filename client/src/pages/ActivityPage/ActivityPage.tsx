import CertificationActivity from '../../components/ActivityComponents/CertificationActivity';
import EndActivity from '../../components/ActivityComponents/EndActivity';
import StartActivity from '../../components/ActivityComponents/StartActivity';
import Select from '../../components/Select/Select';
import ActivityService from '../../service/ActivityService';
import './ActivityPage.css'
import { useEffect, useState } from 'react';

const ActivityPage = () => {

    const [serviceNumber, setServiceNumber] = useState<string>("");
    
    // год-месяц-день
    const [date, setDate] = useState<string>("");
    const [dateError, setDateError] = useState<boolean>(false);

    const [salary, setSalary] = useState<number>(0);

    const [note, setNote] = useState<string>("");

    const [mark, setMark] = useState<number>(0);

    const [type, setType] = useState<string>("");

    const [contentByType, setContentByType] = useState<React.ReactNode>(<></>);

    // data of activities

    const [startActivityData, setStartActivityData] = useState<object>({});
    const [endActivityData, setEndActivityData] = useState<object>({});
    const [certificationActivityData, setCertificationActivityData] = useState<object>({});

    //

    useEffect(() => {
        switch (type) {
            case "Вступление в должность":
                setContentByType(<StartActivity setData={data => setStartActivityData(data)} />)
                break;
            case "Окончание работы в должности":
                setContentByType(<EndActivity serviceNumber={serviceNumber} setData={data => setEndActivityData(data)}/>)
                break;
            case "Аттестация":
                setContentByType(<CertificationActivity setData={data => setCertificationActivityData(data)}/>)
                break;
            case "Обучение":
                setContentByType(<></>)
                break;
            case "Соревнование":
                setContentByType(<></>)
                break;
            case "Мероприятие":
                setContentByType(<></>)
                break;
            case "Окончание испытательного срока":
                setContentByType(<></>)
                break;
            case "Изменение заработной платы":
                setContentByType(<></>)
                break;
            case "Повышение квалификации":
                setContentByType(<></>)
                break;
            case "Начало работы над проектом":
                setContentByType(<></>)
                break;
            case "Окончание работы над проектом":
                setContentByType(<></>)
                break;
            case "Карьерный диалог":
                setContentByType(<></>)
                break;
            case "Нарушение/выговор":
                setContentByType(<></>)
                break;
            default:
                setContentByType(<></>)
        }      
    }, [type])

    const types = [
        "Вступление в должность",
        "Окончание работы в должности",
        "Аттестация",
        "Обучение",
        "Соревнование",
        "Мероприятие",
        "Окончание испытательного срока",
        "Изменение заработной платы",
        "Повышение квалификации",
        "Начало работы над проектом",
        "Окончание работы над проектом",
        "Карьерный диалог",
        "Нарушение/выговор"
    ]

    const checkBaseData = () => {
        if (!checkDate(date)) {
            return false;
        }

        if (serviceNumber === "") {
            return false;
        }

        return true;
    }

    const checkDate = (newDate: string) => {
        const today = new Date();

        // Начальная точка - юридическое основание компании
        const startDate = new Date('2009-05-21');

        const enterDate = new Date(newDate);

        if (enterDate >= startDate && enterDate <= today) {
            setDateError(false);
            return true;
        } else {
            setDateError(true);
            setDate("")
            return false;
        }
    }

    const add = () => {

        if (!checkBaseData()) {
            return;
        }

        const baseData = {
            serviceNumber,
            date,
            salaryChange: salary,
            note,
            mark
        }

        switch (type) {
            case "Вступление в должность":
                ActivityService.postData('start', {
                    ...baseData,
                    ...startActivityData
                });
                break;
            case "Окончание работы в должности":
                ActivityService.postData('end', {
                    ...baseData,
                    ...endActivityData
                })
                break;
            case "Аттестация":
                ActivityService.postData('certification', {
                    ...baseData,
                    ...certificationActivityData
                })
                break;
            case "Обучение":
                console.log("Вы на обучении");
                break;
            case "Соревнование":
                console.log("Участвуете в соревновании");
                break;
            case "Мероприятие":
                console.log("Вы принимаете участие в мероприятии");
                break;
            case "Окончание испытательного срока":
                console.log("Ваш испытательный срок закончился");
                break;
            case "Изменение заработной платы":
                console.log("Изменена заработная плата");
                break;
            case "Повышение квалификации":
                console.log("Вы повышаете квалификацию");
                break;
            case "Начало работы над проектом":
                console.log("Начата работа над проектом");
                break;
            case "Окончание работы над проектом":
                console.log("Завершена работа над проектом");
                break;
            case "Карьерный диалог":
                console.log("Ведется карьерный диалог");
                break;
            case "Нарушение/выговор":
                console.log("Вы нарушили правила");
                break;
            default:
                console.log("Некорректный тип события");
        }
    }

    return (
        <div id="activity__page">
            <div id="activity">
                <h1>Активность</h1>
                <table>
                    <tbody>
                        <tr>
                            <td>Табельный номер:</td>
                            <td>
                                <input 
                                    type='text'
                                    value={serviceNumber}
                                    onChange={e => setServiceNumber(e.target.value)}
                                />
                            </td>
                        </tr>

                        <tr>
                            <td>Дата:</td>
                            <td>
                                <input 
                                    type='date'
                                    value={date}
                                    onChange={e => setDate(e.target.value)}
                                    className={dateError ? 'input_error' : ''}
                                />
                            </td>
                        </tr>
                        
                        <tr>
                            <td>Изменение зарплаты:</td>
                            <td>
                                <input 
                                    type='number'
                                    value={salary}
                                    onChange={e => setSalary(Number(e.target.value))}
                                />
                            </td>
                        </tr>

                        <tr>
                            <td>Небольшая заметка:</td>
                            <td>
                                <input
                                    type='text'
                                    value={note}
                                    onChange={e => setNote(e.target.value)}
                                />
                            </td>
                        </tr>

                        <tr>
                            <td>Оценка:</td>
                            <td>
                                <input
                                    type='number'
                                    value={mark}
                                    onChange={e => setMark(Number(e.target.value))}
                                />
                            </td>
                        </tr>

                        <tr>
                            <td>Вид:</td>
                            <td>
                                <Select options={types} setOption={getType => setType(getType)} />
                            </td>
                        </tr>

                        {
                            contentByType
                        }
                    </tbody>
                </table>

                <button onClick={add}>Добавить</button>
            </div>
        </div>
    );
};
  
export default ActivityPage;