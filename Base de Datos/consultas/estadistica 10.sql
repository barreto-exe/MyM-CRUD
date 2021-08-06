-- Facturas por pagar a proveedores por mes. (en esta fanquicia)

SELECT p.rif_proveedor, p.razon_social, f.num_factura, 
FROM facturas_prov f, ordenes_compras oc, proveedores p
WHERE f.ordenes_compra = oc.cod_orden_c
AND oc.rif_proveedor = p.rif_proveedor
AND oc.franquicia = ?
AND to_char(f.fecha_emision, 'mm') = ?
AND fecha_pago = NULL;