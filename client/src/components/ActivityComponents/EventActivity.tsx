import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const EventActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            place: string, 
            result: string,
            theme: string,
            role: 'participant' | 'speaker' | ''
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
    const [theme, setTheme] = useState<string>("")

    const changePlace = (newPlace: string) => {
        setPlace(newPlace);

        let newData = currentData;
        currentData.place = newPlace;

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

    const changeRole = (newRole: 'слушатель' | 'выступающий') => {

        let newData = currentData;

        if (newRole === 'слушатель') {
            currentData.role = 'participant';
        }

        if (newRole === 'выступающий') {
            currentData.role = 'speaker';
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
                <td>Роль: </td>
                <td>
                    <Select 
                        options={['слушатель', 'выступающий']}
                        setOption={r => changeRole(r as 'слушатель' | 'выступающий')}
                    /> 
                </td>
            </tr>
        </>
    )
}

export default EventActivity;