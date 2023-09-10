using Microsoft.EntityFrameworkCore;

namespace Backend.Models.Repository
{
    //reposotorio responsable de acceder al controlador
    public class MascotaRepository : IMascotaRepository
    {
        public readonly Context context;

        public MascotaRepository(Context context)
        {
            this.context = context;
        }

        public async Task<List<Mascota>> GetListMascotas()
        {
            return await context.Mascotas.ToListAsync();
        }

        public async Task<Mascota> GetMascota(int id)
        {
            return await context.Mascotas.FindAsync(id);
        }

        public async Task DeleteMascota(Mascota mascota)
        {
            context.Mascotas.Remove(mascota);
            await context.SaveChangesAsync();
        }

        public async Task<Mascota> AddMascota(Mascota mascota)
        {
            await context.AddAsync(mascota);
            await context.SaveChangesAsync();
            return mascota;
        }

        public async Task UpdateMascota(Mascota mascota)
        {
            var mascotaItem = await context.Mascotas.FirstOrDefaultAsync(x => x.Id == mascota.Id);

            if (mascotaItem != null)
            {
                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Color = mascota.Color;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Peso = mascota.Peso;

                await context.SaveChangesAsync();
            }
        }

    }
}
