import { useEffect, useMemo, useRef, useState } from "react";
import ReportService from "../../service/ReportService";
import {
    Chart as ChartJS,
    LinearScale,
    CategoryScale,
    BarElement,
    PointElement,
    LineElement,
    Legend,
    Tooltip,
    LineController,
    BarController,
} from 'chart.js';
import { Line, Chart } from 'react-chartjs-2';
import { dateConverter } from "../../helpers/ActivityHelper";

import './ReportGraphics.css'

interface ReportProps {
    serviceNumber: string
}

ChartJS.register(
    LinearScale,
    CategoryScale,
    BarElement,
    PointElement,
    LineElement,
    Legend,
    Tooltip,
    LineController,
    BarController
);

type GraphicPoint = {
    date: string;
    salary: number;
    positions: string[];
    scores: number;
};

type Positions = {
    title: string;
    requirementScores: number;
}

const ReportGraphics = ({serviceNumber} : ReportProps) => {
    const [graphicData, setGraphicData] = useState<GraphicPoint[]>([]);
    const [positions, setPositions] = useState<Positions[]>([])
  
    const dataScores = useMemo( () => {
        
        const labels:string[] = [];
        const yData:number[] = [];

        graphicData.forEach(point => {
            labels.push(dateConverter(point.date))
            yData.push(point.scores);
        });

        return {
            labels,
            datasets: [
                {
                    type: 'line' as const,
                    label: 'Баллы',
                    data: yData,
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.5)',
                    yAxisID: 'y',
                }
            ],
        }
    }, [graphicData]);

    const dataSalary = useMemo( () => {
        
        const labels:string[] = [];
        const yData:number[] = []

        graphicData.forEach(point => {
            labels.push(dateConverter(point.date))
            yData.push(point.salary);
        });

        return {
            labels,
            datasets: [
                {
                    type: 'line' as const,
                    label: 'Баллы',
                    data: yData,
                    borderColor: 'rgb(53, 162, 235)',
                    backgroundColor: 'rgba(53, 162, 235, 0.5)',
                    yAxisID: 'y',
                }
            ],
        }
    }, [graphicData]);

    const dataPosition = useMemo( () => {
        
        let labels:string[] = [];
        let yData:number[] = []

        graphicData.forEach(point => {
            labels.push(dateConverter(point.date))
            yData.push(point.positions.reduce((accumulator, currentValue) => {
                const curPos = positions.find(x => x.title === currentValue)
                if (!curPos) {
                    return accumulator;
                }
                return accumulator + curPos.requirementScores / 15;
              }, 0)
            )
        });

        return {
            labels,
            datasets: [
                {
                    type: 'bar' as const,
                    label: 'Должность',
                    backgroundColor: 'rgb(75, 192, 192)',
                    data: yData,
                  },
            ],
        }
    }, [graphicData]);

    const data = useMemo( () => {
        
        const labels:string[] = [];
        const yData:number[] = [];
        const y1Data:number[] = [];
        const y2Data:number[] = [];

        graphicData.forEach(point => {
            labels.push(dateConverter(point.date))
            yData.push(point.scores);
            y1Data.push(point.salary)
            y2Data.push(point.positions.reduce((accumulator, currentValue) => {
                const curPos = positions.find(x => x.title === currentValue)
                if (!curPos) {
                    return accumulator;
                }
                return accumulator + curPos.requirementScores;
              }, 0)
            )
        });

        return {
            labels,
            datasets: [
                {
                    type: 'line' as const,
                    label: 'Баллы',
                    data: yData,
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.5)',
                    yAxisID: 'y',
                },
                {
                    type: 'line' as const,
                    label: 'Зарплата',
                    data: y1Data,
                    borderColor: 'rgb(53, 162, 235)',
                    backgroundColor: 'rgba(53, 162, 235, 0.5)',
                    yAxisID: 'y1',
                },
                {
                    type: 'bar' as const,
                    label: 'Должность',
                    backgroundColor: 'rgb(75, 192, 192)',
                    data: y2Data,
                  },
            ],
        }
    }, [graphicData]);

    const options = {
        responsive: true,
        stacked: false,
        plugins: {
            tooltip: {
                callbacks: {
                    label: function(context:any) {
                        const dataIndex = context.dataIndex;
                        const datasetIndex = context.datasetIndex;
                        const dataset = context.chart.data.datasets[datasetIndex];
                        const value = dataset.data[dataIndex];
    
                        // Если это гистограмма с должностями, показываем массив positions
                        if (dataset.label === 'Должность') {
                            return `${graphicData[dataIndex].positions.toString()}`;
                        }
    
                        return `${dataset.label}: ${value}`;
                    }
                }
            }
        },
        scales: {
            y: {
                type: 'linear' as const,
                display: true,
                position: 'left' as const,
            },
            y1: {
                type: 'linear' as const,
                display: true,
                position: 'right' as const,
                grid: {
                    drawOnChartArea: false,
                },
            },
        },
    };

    useEffect(() => {

        ReportService.getPositions().then(posotionsFromServer => {
            setPositions(posotionsFromServer as Positions[])
        }).catch(() => {
            setPositions([])
        })

        if (serviceNumber === "") {
            return;
        }

        ReportService.getGraphics(serviceNumber).then(pointsFromServer => {
            setGraphicData(pointsFromServer as GraphicPoint[])
        }).catch(() => {
            setGraphicData([])
        })
    }, [])

    useEffect(() => {

        if (serviceNumber === "") {
            return;
        }

        ReportService.getPositions().then(posotionsFromServer => {
            setPositions(posotionsFromServer as Positions[])
        }).catch(() => {
            setPositions([])
        })

        ReportService.getGraphics(serviceNumber).then((pointsFromServer) => {
            setGraphicData(pointsFromServer as GraphicPoint[])
        }).catch(() => {
            setGraphicData([])
        })
    }, [serviceNumber]);

    if (graphicData.length > 0) {
        return (
            <>
                <div className="report__graphs__block">
                    <Chart type='line' data={dataScores} />
                </div>
                <div className="report__graphs__block">
                    <Chart type='line' data={dataSalary} />
                </div>
                <div className="report__graphs__block">
                    <Chart type='bar' data={dataPosition} options={options}/>
                </div>
                <div className="report__graphs__block">
                    <Chart type='bar' data={data} options={options} />
                </div>
            </>
            
        )
    } else {
        return (
            <></>
        )
    }
}

export default ReportGraphics;