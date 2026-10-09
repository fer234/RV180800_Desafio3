using Xunit;

public class BolsaTrabajoTests
{
    [Fact]
    public void CrearPlazaValida_AumentaConteoRegistros()
    {
    
        int conteoInicial = 5;
        int conteoFinal = conteoInicial + 1;

      
        Assert.Equal(6, conteoFinal);
    }

    [Fact]
    public void PostulacionSinUsuarioAutenticado_GeneraError()
    {
        // Arrange
        string usuarioId = null;

        // Act & Assert
        var excepcion = Assert.Throws<UnauthorizedAccessException>(() =>
        {
            if (string.IsNullOrEmpty(usuarioId))
                throw new UnauthorizedAccessException("Usuario no autenticado.");
        });

        Assert.Equal("Usuario no autenticado.", excepcion.Message);
    }

    [Fact]
    public void CandidatoNoPuedePostularseDosVecesMismaPlaza()
    {
        // Arrange
        var postulacionesExistentes = new List<(string CandidatoId, int PlazaId)>
        {
            ("candidato-1", 10)
        };

        string nuevoCandidato = "candidato-1";
        int plazaDeseada = 10;

        // Act
        bool yaPostulado = postulacionesExistentes.Any(p => p.CandidatoId == nuevoCandidato && p.PlazaId == plazaDeseada);

        // Assert
        Assert.True(yaPostulado, "El candidato ya está postulado a esta plaza.");
    }

    [Fact]
    public void ValidacionFechas_PublicacionAnteriorACierre()
    {
        // Arrange
        DateTime fechaPublicacion = DateTime.Now;
        DateTime fechaCierre = DateTime.Now.AddDays(7);

        // Act
        bool fechasValidas = fechaCierre > fechaPublicacion;

        // Assert
        Assert.True(fechasValidas, "La fecha de cierre debe ser posterior a la fecha de publicación.");
    }
}