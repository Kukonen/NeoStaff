import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

interface EndActivityProps extends ActivityComponentsProps {
    serviceNumber: string
}

const EndActivity = ({setData, serviceNumber}: EndActivityProps) => {
    const [positions, setPositions] = useState<string[]>([])

    const [selectedPosition, setSelectPositions] = useState<string>()

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getEnd(serviceNumber).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
    }, [])

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getEnd(serviceNumber).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
        
    }, [serviceNumber])

    const selectPosotion = (pos: string) => {
        setSelectPositions(pos);
        setData({
            position: pos
        })
    }

    return (
        <>
        {
            positions.length > 0 &&
            <tr>
                <td>Позиция: </td>
                <td>
                    <Select 
                        options={positions}
                        setOption={pos => selectPosotion(pos)}
                    /> 
                </td>
            </tr>
        }
        </>
    )
}

export default EndActivity;