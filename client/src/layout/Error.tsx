import { Link } from 'react-router-dom';

interface ErrorProps {
    errorMessage: string
}

const Error = ({errorMessage} : ErrorProps) => {
    return (
        <div>
            {errorMessage}
        </div>
    );
};

export default Error;