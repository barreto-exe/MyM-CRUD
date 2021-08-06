-- Histórico de uso de servicio por vehículo (en esta franquicia)
-- Placa, servicio, fecha y ficha

SELECT r.fecha_ent, s.nombre_S, os.num_Ficha
INTO TEMP R1
FROM ordenes_servicio os, registros r, servicios s
WHERE os.num_Ficha = r.num_Ficha
AND os.cod_Servicio = s.cod_Servicio
AND r.franquicia = ?
AND r.placa_vehiculo = ?
GROUP BY 3, 2, 1
ORDER BY 1 ASC;
