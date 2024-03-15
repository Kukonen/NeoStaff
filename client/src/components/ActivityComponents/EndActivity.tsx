import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

interface EndActivityProps extends ActivityComponentsProps {
    serviceNumber: string
    date: string;
}

const EndActivity = ({setData, serviceNumber, date}: EndActivityProps) => {
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
        if (serviceNumber !== "") {
            ActivityService.getPositions(serviceNumber, date).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
    }, [])

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getPositions(serviceNumber, date).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
        
    }, [serviceNumber])

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
                    options={positions as string[]}
                    setOption={pos => selectPosotion(pos)}
                /> 
            </td>
        </tr>
    )
}

export default EndActivity;