using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;

namespace Puc.Negocios_C
{

    public class logErro
    {
        Negocio.clBanco banco = new clBanco();
        public void GravarLog(Exception ex)
        {

            banco.parametros.Clear();
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("source", ex.Source));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("trace", ex.StackTrace.ToString()));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("modulo", ex.TargetSite.Module.Name));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("metodo", ex.TargetSite.MethodHandle.Value));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("modulo_versao", ex.TargetSite.ReflectedType.Module.ModuleVersionId));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("fullname", ex.TargetSite.Module.FullyQualifiedName));
            banco.parametros.Add(new System.Data.SqlClient.SqlParameter("namespace", ex.TargetSite.Module.Assembly.FullName));
            banco.ExecuteAndReturnData("sp_LogErrorIncluir", "tablog");

        }
    }


}
