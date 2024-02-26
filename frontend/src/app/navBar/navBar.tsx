import { Button, ButtonGroup } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import './navBar.css'

export function NavBar() {
  const navigate = useNavigate();

  return (
    <div className='nav-bar'>
      <ButtonGroup variant="contained" aria-label="Basic button group">
        <Button onClick={() => {navigate("/tickets/new")}} >Criar ticket</Button>
        <Button onClick={() => {navigate("/tickets")}} >Listar tickets</Button>
      </ButtonGroup>
    </div>
  );
}