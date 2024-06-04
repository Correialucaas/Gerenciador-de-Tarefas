public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<TarefasContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddControllers();
}


[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly TarefasContext _context;

    public TarefasController(TarefasContext context)
    {
        _context = context;
    }

    // GET: api/Tarefas
    [HttpGet]
    public IEnumerable<Tarefa> GetTarefas()
    {
        return _context.Tarefas.ToList();
    }

    // GET: api/Tarefas/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(tarefa);
    }

    // POST: api/Tarefas
    [HttpPost]
    public async Task<IActionResult> PostTarefa([FromBody] Tarefa tarefa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTarefa", new { id = tarefa.Id }, tarefa);
    }

    // PUT: api/Tarefas/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarefa(int id, [FromBody] Tarefa tarefa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != tarefa.Id)
        {
            return BadRequest();
        }

        _context.Entry(tarefa).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Tarefas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
        {
            return NotFound();
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}