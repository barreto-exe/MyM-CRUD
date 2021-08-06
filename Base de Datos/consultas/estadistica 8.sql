-- Clientes que hacen reservas y después no usan el servicio

SELECT  reserva, fecha
INTO TEMP R1
FROM apartan
GROUP BY 1, 2;

SELECT c.cedula_c, c.nombre_c, r.num_reserva, r.fecha_emision
FROM reservas r, clientes c, R1
WHERE r.cliente = c.cedula_c
AND r.num_reserva = R1.reserva
AND R1.fecha > CURRENT_DATE
AND NOT EXISTS (SELECT reg.*
                FROM registros reg
                WHERE reg.num_reserva = r.num_reserva);