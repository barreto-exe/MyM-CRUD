using MyM_CRUD.DataBase;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyM_CRUD.Model
{
    public class Branch
    {
        public string BranchRif { get; set; }
        public string BranchName { get; set; }
        public string BranchDirection { get; set; }
        public string BranchManagerName { get; set; }
        public string BranchCity { get; set; }

        public static Branch GetBranch(string branchRif)
        {
            Branch branch = new Branch();

            if(branchRif != null && branchRif != "")
            {
                string query =
                    "SELECT * " +
                    "FROM franquicias f, personas p " +
                    "WHERE " +
                    "f.ced_encargado = p.cedula_p AND " +
                    "f.rif_franquicia = @rif ";
                PostgreOp op = new PostgreOp(query);
                op.PasarParametros("rif", branchRif);

                try
                {
                    NpgsqlDataReader dr = op.EjecutarReader();
                    if (dr.Read())
                    {
                        branch.BranchRif = dr["rif_franquicia"].ToString();
                        branch.BranchName = dr["nombre_f"].ToString();
                        branch.BranchDirection = dr["direccion_f"].ToString();
                        branch.BranchManagerName = dr["nombre_p"].ToString();
                        branch.BranchCity = dr["nom_ciudad"].ToString();
                    }
                    dr.Close();
                }
                catch (Exception ex)
                {
                }
              
            }

            return branch;
        }
    }
}
