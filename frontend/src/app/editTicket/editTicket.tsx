import { useNavigate, useParams } from "react-router-dom";
import { Produto } from '../../dataModels/Produto';
import { Button, FormControl, InputLabel, MenuItem, Select, Stack, TextField } from '@mui/material';
import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import "./editTicket.css";
import { TicketService } from "../../service/ticketService";
import { ProdutoService } from "../../service/produtoService";

const staticProducts: Produto[] = [{
	id: 1,
	nome: "Produto 1"
	},
	{
		id: 2,
		nome: "Produto 2"
	},
	{
		id: 3,
		nome: "Produto 3"
	}
];

type FormValues = {
  titulo: string,
  desc: string,
  prioridade: string,
  produto: string
}

export function EditTicket() {

  const navigate = useNavigate();

	const [products, setProducts] = useState<Produto[]>([]);

  const form = useForm<FormValues>({
    defaultValues: {
      titulo: '',
      desc: '',
      prioridade: '',
      produto: ''
    }
  });

  const {register, handleSubmit, formState} = form;

  const {errors} = formState;

  useEffect(() => { 
    const productServer = new ProdutoService();

    productServer.getProdutos().then((produtos) => setProducts(produtos));
 	}, []); 
	
	let {id} = useParams();

	function handleEditClick(data : FormValues) {
    
    if(!id) return;

    const ticketService = new TicketService();
    ticketService.updateTicket( parseInt(id),
      {
        titulo: data.titulo,
        descricao: data.desc,
        prioridade: parseInt(data.prioridade),
        produtoId: parseInt(data.produto)
      }
    )
		
	}

  function handleGoBackClick() {
    navigate('/tickets/' + id);
  }
  
  
  return (
    <form onSubmit={handleSubmit(handleEditClick)}>
      <Stack spacing={1.5} width={400}>
        
        <h2>Editar ticket</h2> 

        <TextField id="titulo" label="Título" 
        {...register("titulo")}
        error={!!errors.titulo}
        helperText={errors.titulo?.message}
        />

        <TextField id="desc" label="Descrição" 
          {...register("desc")}
          error={!!errors.desc}
          helperText={errors.desc?.message}
        />
        
        <FormControl fullWidth>
          <InputLabel id="prioridade-select-label">Prioridade</InputLabel>
          <Select 
          labelId="prioridade-select-label" 
          id="prioridade-select"
          {...register("prioridade")}
          >
            <MenuItem value='1'>1</MenuItem>
            <MenuItem value='2'>2</MenuItem>
            <MenuItem value='3'>3</MenuItem>
            <MenuItem value='4'>4</MenuItem>
            <MenuItem value='5'>5</MenuItem>
          </Select>
        </FormControl>
      

        <FormControl fullWidth>
          <InputLabel id="produto-select-label">Produto</InputLabel>
          <Select 
            labelId="produto-select-label" 
            id="produto-select" 
            {...register("produto")}
          >
            {products.map((product) => (
              <MenuItem key={product.id} value={""+product.id}>{product.nome}</MenuItem>
            ))}
          </Select>
        </FormControl>

        <Button type="submit" variant="contained">Editar</Button>

        <Button variant="contained" onClick={handleGoBackClick}>Voltar</Button>

      </Stack>  
    </form>
  );
}