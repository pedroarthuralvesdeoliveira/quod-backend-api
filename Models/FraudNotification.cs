namespace quod_backend_api.Models;

public record FraudNotification(
    string TransacaoId,
    string TipoBiometria,
    string TipoFraude,
    DateTime DataCaptura,
    DispositivoInfo? Dispositivo,
    List<string>? CanalNotificacao,
    string NotificadoPor,
    MetadadosInfo? Metadados    
);