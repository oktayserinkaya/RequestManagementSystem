using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Interface;
using DTO.Concrete.EmployeeDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

public class EmployeeManager : BaseManager<IEmployeeRepository, Employee>, IEmployeeManager
{
    private readonly IEmployeeRepository _repo;
    private readonly IMapper _mapper;

    public EmployeeManager(IEmployeeRepository service, IMapper mapper) : base(service, mapper)
    {
        _repo = service;
        _mapper = mapper;
    }

    // AppUserId ile Employee + Department + Title çek
    public async Task<GetEmployeeDTO?> GetWithDepartmentByAppUserIdAsync(Guid appUserId)
    {
        var entity = await _repo.GetByDefaultAsync(
            e => e.AppUserId == appUserId,
            join: q => q.Include(e => e.Department!)
                        .Include(e => e.Title!)
        );

        return _mapper.Map<GetEmployeeDTO?>(entity);
    }
}
