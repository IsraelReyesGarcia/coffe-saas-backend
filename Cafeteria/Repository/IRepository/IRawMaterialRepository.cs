using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.RawMaterial;

namespace Cafeteria.Repository.IRepository;

public interface IRawMaterialRepository
{
    // Obtener todas las materias primas
    ICollection<RawMaterial> GetRawMaterials();

    // Obtener una materia prima
    RawMaterial GetRawMaterial(int id);

    // Saber si existe una materia prima
    bool RawMaterialExists(int id);

    // Crear materia prima
    bool CreateRawMaterial(RawMaterial rawMaterial);

    // Actualizar materia prima
    bool UpdateRawMaterial(RawMaterial rawMaterial);

    // Eliminar materia prima
    bool DeleteRawMaterial(RawMaterial rawMaterial);

    // Guardar cambios
    bool Save();
}
