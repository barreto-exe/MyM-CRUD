-- Producto con mayor/menos salida por ventas (en esta franquicia)

-- Producto con menos salida por ventas 

SELECT r.iendaProducto, SUM(r.cantComprada) AS suma
INTO TEMP R1
FROM registran r, 
WHERE 
