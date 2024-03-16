import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const CompetitionActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            place: string, 
            result: string,
            theme: string,
            role: 'participant' | 'organizer' | 'jury' | ''
        }
    >(
        {
            place: "", 
            result: "",
            role: "",
            theme: ""
        }
    );
    const [place, setPlace] = useState<string>("")
    const [result, setResult] = useState<string>("")
    const [theme, setTheme] = useState<string>("")

    const changePlace = (newPlace: string) => {
        setPlace(newPlace);

        let newData = currentData;
        currentData.place = newPlace;

        setCurrentData(newData);

        setData(newData);
    }

    const changeResult = (newResult: string) => {
        setResult(newResult);

        let newData = currentData;
        currentData.result = newResult;

        setCurrentData(newData);

        setData(newData);
    }

    const changeTheme = (newTheme: string) => {
        setTheme(newTheme);

        let newData = currentData;
        currentData.theme = newTheme;

        setCurrentData(newData);

        setData(newData);
    }

    const changeRole = (newRole: 'участник' | 'организатор' | 'член жюри') => {

        let newData = currentData;

        if (newRole === 'участник') {
            currentData.role = 'participant';
        }

        if (newRole === 'организатор') {
            currentData.role = 'organizer';
        }

        if (newRole === 'член жюри') {
            currentData.role = 'jury';
        }

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td>Место соревнования: </td>
                <td>
                    <input 
                        type="text"
                        value={place}
                        onChange={e => changePlace(e.target.value)}
                    />
                </td>
            </tr>

            <tr>
                <td>Тема соревнования: </td>
                <td>
                    <input 
                        type="text"
                        value={theme}
                        onChange={e => changeTheme(e.target.value)}
                    />
                </td>
            </tr>
            
            <tr>
                <td>Результат соревнования: </td>
                <td>
                    <input 
                        type="text"
                        value={result}
                        onChange={e => changeResult(e.target.value)}
                    />
                </td>
            </tr>
            <tr>
                <td className="td_top-diection">Роль: </td>
                <td>
                    <Select 
                        options={['участник', 'организатор', 'член жюри']}
                        setOption={r => changeRole(r as 'участник' | 'организатор' | 'член жюри')}
                    /> 
                </td>
            </tr>
        </>
    )
}

export default CompetitionActivity;