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
            //@ts-ignore
            const newPositions = positionFromServer.map(p => p.title)
            console.log(newPositions)
            setPositions(newPositions);
        }).then(err => console.log(err));
    }, [])

    const selectPosotion = (pos: string) => {
        let newData = currentData;
        currentData.position = pos;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <tr>
            <td className="td_top-diection">
                Позиция
            </td>
            <td>
                {
                    positions.length > 0 ? 
                    <Select 
                        options={positions}
                        setOption={pos => selectPosotion(pos)}
                    /> : null
                }
                {/* <Select 
                    options={positions}
                    setOption={pos => selectPosotion(pos)}
                />  */}
            </td>
        </tr>
    )
}

export default StartActivity;