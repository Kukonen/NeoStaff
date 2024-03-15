import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const StartActivity = ({setData}: ActivityComponentsProps) => {
    const [positions, setPositions] = useState<string[]>([])

    const [selectedPosition, setSelectPositions] = useState<string>()

    useEffect(() => {
        ActivityService.getStart().then(positionFromServer => {
            setPositions(positionFromServer as string[]);
        })
    }, [])

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

export default StartActivity;