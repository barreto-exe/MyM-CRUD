
ALTER TABLE facturas
ADD CONSTRAINT jerarquia2_facturas
CHECK ((tipo_fact='S' AND EXIST (facturas_servicio)
                      AND NOT EXIST (facturas_tienda))
        OR
        (tipo_fact='T' AND EXIST (facturas_tienda)
                       AND NOT EXIST (facturas_servicio)));


ALTER TABLE productos
ADD CONSTRAINT jerarquia2_productos
CHECK ((tipo_p='T' AND EXIST (tienda_productos)
                   AND NOT EXIST (eco_productos))
        OR
        (tipo_p='E' AND EXIST (eco_productos)
                    AND NOT EXIST (tienda_productos)));

ALTER TABLE solicitan
ADD CONSTRAINT chk_mismo_proveedor
CHECK ((SELECT o.rif_proveedor
        FROM ordenes_compra o
        WHERE o.cod_orden_c = s.cod_orden_compra)

        IN
    
        (SELECT d.rif_proveedor
        FROM distribuyen d
        WHERE d.cod_producto = s.cod_producto));

                    