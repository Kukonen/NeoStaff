import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <header>
        <nav>
            {/* <div>

            </div>
            <div>
                
            </div> */}
            <Link to="/activity">Активности</Link>
            <Link to="/report">Отчёт</Link>
        </nav>
    </header>
  );
};

export default Header;