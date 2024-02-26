import logo from '../../images/2.png';
import './homePage.css';

export function HomePage() {
    console.log(logo);
    return (
        <div className="container">
            <div className="image-container">
                <img src={logo} alt="Your Image" />
            </div>
            <div className="description">
                <h2>Sistema de Suporte de Tickets</h2>
                <p>2024 challenge</p>
            </div>
        </div>
    )
}