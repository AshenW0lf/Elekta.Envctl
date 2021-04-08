namespace Elekta.Envctl.Models.Docker
{
    public class Server
    {
        public Engine Engine { get; set; }
        public Containerd Containerd { get; set; }
        public Runc Runc { get; set; }
        public DockerInit DockerInit { get; set; }
    }
}