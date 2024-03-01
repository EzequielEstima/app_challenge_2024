import { useNavigate, useParams } from "react-router-dom";
import { Produto } from '../../dataModels/Produto';
import { Button, FormControl, InputLabel, MenuItem, Select, Stack, TextField } from '@mui/material';
import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import "./editTicket.css";
import { TicketService } from "../../service/ticketService";
import { ProdutoService } from "../../service/produtoService";

type FormValues = {
  titulo: string,
  desc: string,
  prioridade: string,
  produto: string
}

const abortController = new AbortController();

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

  let {id} = useParams();

  useEffect(() => { 

    if(!id) return;

    const productService = new ProdutoService();
    productService.getProdutos(abortController)
    .then((listaProdutosModel) => setProducts(listaProdutosModel.products))
    .catch((error) =>{
      if (error.code === 'ERR_NETWORK') {
        abortController.abort(); // Abort other requests
        alert('Não foi possível ligar ao servidor');
        navigate('/')
      } else if(error.code !== "ERR_CANCELED"){ // Ignore aborted requests
        alert(`Não foi possível obter a lista de produtos \n\n ERRO : ${error.response.data}`)
        navigate('/tickets')
      }
    });

    const ticketService = new TicketService();
    ticketService.getTicketById(parseInt(id))
    .then((ticket) => {
      form.setValue('titulo', ticket.titulo);
      form.setValue('desc', ticket.descricao);
      form.setValue('prioridade', ticket.prioridade.toString());
      form.setValue('produto', ticket.produtoId.toString());
    })
    .catch((error) => {
      if (error.code === 'ERR_NETWORK') {
        abortController.abort(); // Abort other requests
        alert('Não foi possível ligar ao servidor');
        navigate('/')
      } else if(error.code !== "ERR_CANCELED"){ // Ignore aborted requests
        alert(`Não foi possível obter os dados do o ticket \n\n ERRO : ${error.response.data}`)
        navigate('/tickets')
      }
    });
 	}, []); 
	

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
    ).then(() => {
      alert('Ticket editado com sucesso');
    }).catch((error) => {
      if (error.code === 'ERR_NETWORK') {
        alert('Não foi possível ligar ao servidor');
      } else {
        alert(`Erro ao editar ticket\n\nERRO : ${error.response.data}`);
      }
    });
		
	}

  function handleGoBackClick() {
    navigate('/tickets/' + id);
  }
  
  
  return (
    <form onSubmit={handleSubmit(handleEditClick)}>
      <Stack spacing={1.5} width={400}>
        
        <h2>Editar ticket</h2> 

        <TextField id="titulo" label="Título" 
        {...register("titulo", {required: "Título é obrigatório"})}
        error={!!errors.titulo}
        helperText={errors.titulo?.message}
        InputLabelProps={{ shrink: true }}
        />

        <TextField id="desc" label="Descrição" 
          {...register("desc", {required: "Descrição é obrigatória"})}
          error={!!errors.desc}
          helperText={errors.desc?.message}
          InputLabelProps={{ shrink: true }}
        />
        
        <FormControl fullWidth>
          <InputLabel id="prioridade-select-label">Prioridade</InputLabel>
          <Select 
          labelId="prioridade-select-label" 
          id="prioridade-select"
          {...register("prioridade", {required: "Prioridade é obrigatória"})}
          value={form.watch("prioridade")}
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
            {...register("produto", {required: "Produto é obrigatório"})}
            value={form.watch("produto")}
          >
            {products.map((product) => (
              <MenuItem key={product.productId} value={`${product.productId}`}>{product.nome}</MenuItem>
            ))}
          </Select>
        </FormControl>

        <Button type="submit" variant="contained">Guardar</Button>

        <Button variant="contained" onClick={handleGoBackClick}>Voltar</Button>

      </Stack>  
    </form>
  );
}