-- Marca de vehículos que más atendemos por tipo de servicio

SELECT s.nombre_S, ma.nom_marca, COUNT(r.*) AS cantidad
INTO TEMP R1
FROM registros r, ordenes_servicio os, servicios s,
     vehiculos v, modelos mo, marcas ma
WHERE r.placa_Vehiculo = v.placa
AND v.nom_modelo = mo.nom_modelo
AND mo.cod_marca = ma.cod_Marca
AND r.num_Ficha = os.num_Ficha
AND os.cod_Servicio = s.cod_Servicio
AND r.franquicia = ?
GROUP BY 1, 2;

SELECT R1.nombre_S, MAX(R1.cantidad) AS maximo
INTO TEMP R2
FROM R1
GROUP BY 1;

SELECT R1.*
FROM R1, R2
WHERE R1.nombre_S = R2.nombre_S
AND R1.cantidad = R2.maximo
ORDER BY R1.nombre_s;