import { useEffect, useMemo, useState, useRef } from "react";

import './Select.css'
import SelectItem from "./SelectItem";

interface SelectProps {
    options: string[],
    setOption: (value: string) => void
}

const Select = ({options, setOption} : SelectProps) => {
    const container = useRef<any>();
    const [currentOption, setCurrentOption] = useState<string>("");

    const [modalVisible, setModalVisible] = useState<boolean>(false);
    const [modalDisplayNone, setModalDisplayNone] = useState<boolean>(false);

    const filteredOptions = useMemo(() => {
        return options.filter(op => op.toLowerCase().includes(currentOption.toLowerCase()))
    }, [currentOption])

    useEffect(() => {
        const handler = (event:any) => {
            if (!container.current.contains(event.target)) {
                changeVisible(false);
            }
        };
        document.addEventListener("click", handler);
        return () => {
            document.removeEventListener("click", handler);
        };
    });

    const runSelect = () => {
        if (options.includes(currentOption)) {
            setOption(currentOption);
        }
    }

    const setText = (text: string) => {
        setCurrentOption(text)
    }

    const changeVisible = (state: boolean) => {
        setModalDisplayNone(state)
        setTimeout(() => {
            setModalVisible(state)
        }, 1)
    }

    return (
        <div>
            <div ref={container} className="select__modal__section">
                <input 
                    type="text" 
                    value={currentOption}
                    onChange={e => setCurrentOption(e.target.value)}
                    onFocus={() => changeVisible(true)}
                    autoComplete="option"
                />
                
                <button
                    onClick={runSelect}
                >
                    Выбрать
                </button>
            </div>
        
            <div
                style={
                    {
                        position: 'relative',
                        display: modalDisplayNone ? 'block' : 'none'
                    }
                }
                className={modalVisible ? "select__modal__visible" : "select__modal__hidden"}
                
            >
                {
                    filteredOptions.map(option => {
                        return (
                            <SelectItem
                                key={option} 
                                text={option} 
                                setText={setText}
                            />)
                    })
                }
            </div>
        </div>
    )
}

export default Select;