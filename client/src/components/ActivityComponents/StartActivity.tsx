import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const StartActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            position: string
        }
    >(
        {
            position: ""
        }
    );
    
    const [positions, setPositions] = useState<string[]>([])

    useEffect(() => {
        ActivityService.getAllPositions().then(positionFromServer => {
            setPositions(positionFromServer as string[]);
        })
    }, [])

    const selectPosotion = (pos: string) => {
        let newData = currentData;
        currentData.position = pos;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <tr>
            <td>Позиция: </td>
            <td>
                <Select 
                    options={positions}
                    setOption={pos => selectPosotion(pos)}
                /> 
            </td>
        </tr>
    )
}

export default StartActivity;