import './Select.css'

interface SelectItem {
    text: string
    setText: (text: string) => void
}

const SelectItem = ({text, setText} : SelectItem) => {
    return (
        <div className="select__item"
            onClick={() => {
                setText(text)
            }}
        >
            {
                text
            }
        </div>
    )
}

export default SelectItem;