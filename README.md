CRUD de Medicamentos

el proyecto de fontend y la base de datos estan subidos a un hosting llamado somee este es el link de acceso
https://medigroup.somee.com/

El FrontEnd esta hecho con HTML, jquery y bootstrap, consta de una sola pantalla donde se pueden consultar, agregar, modificar y eliminar medicamentos
para las llamadas asincronas hacia la API se usa ajax

para acceder a la base de datos se puedes acceder desde SQL management studio con las siguientes credenciales:

Servidor: medigroup.mssql.somee.com

Usuario: rafasos851_SQLLogin_1

Contraseña: qzod52vnzw

el proyecto de backend esta subido a otro hosting llamado MonsterASP.NET
es el siguiente link
https://medigroup.runasp.net/api/medicamentos

el controlador consta de 4 metodos 

GET -  para consulta de medicamentos por nombre, categoria y fecha de expiracion

POST - para Agregar un nuevo medicamento  es necesario enviar la informacion en formato json de la siguiente manera
{
  "nombre": "Paracetamol",
  "categoria": "Analgesico",
  "cantidad": 100,
  "fechaExpiracion": "2028-12-04T03:27:10.209Z"
}

PUT - para modificar un medicamento y tambien es necesario enviar los datos en formato JSON como el metodo anterior en el body del Request, pero en la URL poner el ID
{
  "id":"12"
  "nombre": "Paracetamol",
  "categoria": "Analgesico",
  "cantidad": 100,
  "fechaExpiracion": "2028-12-04T03:27:10.209Z"
}

DELETE - para eliminar un medicamento se manda como parametro solo el ID del medicamento
