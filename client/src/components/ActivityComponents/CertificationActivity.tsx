import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const CertificationActivity = ({setData}: ActivityComponentsProps) => {
    const [certifications, setCertifications] = useState<{ id: string, title: string } []>([])

    const [result, setResult] = useState<string>("");

    const [currentData, setCurrentData] = useState<{certification: string, result: string}>({certification: "", result: ""});

    useEffect(() => {
        ActivityService.getCertification().then(certificationFromServer => {
            setCertifications(certificationFromServer as { id: string, title: string } []);
        }).catch(err => console.log(err))
    }, [])

    const selectCertification = (cert: string) => {
        const newSelectedCert = certifications.find(c => c.title === cert);
        
        if (!newSelectedCert) {
            return
        }

        let newCurrentData = currentData;
        newCurrentData.certification = newSelectedCert.id;

        setCurrentData(newCurrentData);
        setData(newCurrentData)
    }

    const changeResult = (newResult: string) => {
        setResult(newResult)
        
        let newCurrentData = currentData;
        newCurrentData.result = newResult;

        setCurrentData(newCurrentData);
        setData(newCurrentData)
    }

    return (
        <>
            <tr>
                <td>Аттестация: </td>
                    <td className="td_top-diection">
                        <Select 
                            options={certifications.map(cert => cert.title)}
                            setOption={cert => selectCertification(cert)}
                        /> 
                    </td>
                </tr>
                <tr>
                <td>Результат: </td>
                <td>
                    <input 
                        type="text"
                        value={result}
                        onChange={e => changeResult(e.target.value)}
                    /> 
                </td>
            </tr>
        </>
    )
}

export default CertificationActivity;